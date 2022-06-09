using Microsoft.EntityFrameworkCore;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using DesignGear.Common.Exceptions;
using DesignGear.Contracts.Communicators.Interfaces;
using DesignGear.ConfigManager.Core.Services.Interfaces;
using DesignGear.ConfigManager.Core.Data;
using DesignGear.Contracts.Dto.ConfigManager;
using DesignGear.ConfigManager.Core.Data.Entity;
using Newtonsoft.Json;
using DesignGear.ModelPackage;
using DesignGear.ConfigManager.Core.Storage.Interfaces;
using DesignGear.Common.Extensions;
using DesignGear.Contracts.Enums;
using DesignGear.Contracts.Dto;
using ParameterDefinitionDto = DesignGear.Contracts.Dto.ConfigManager.ParameterDefinitionDto;
using System.Transactions;
using static DesignGear.ModelPackage.DesignGearModelPackage;
using System.IO.Compression;

namespace DesignGear.ConfigManager.Core.Services
{
    public class ConfigurationService : IConfigurationService
    {
        private readonly IMapper _mapper;
        private readonly HttpClient _httpClient;
        private readonly DataAccessor _dataAccessor;
        private readonly IConfigurationFileStorage _configurationFileStorage;

        public ConfigurationService(IMapper mapper,
            DataAccessor dataAccessor,
            IHttpClientFactory clientFactory,
            IConfigurationFileStorage configurationFileStorage)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _dataAccessor = dataAccessor ?? throw new ArgumentNullException(nameof(dataAccessor));
            _httpClient = clientFactory.CreateClient();
            _configurationFileStorage = configurationFileStorage ?? throw new ArgumentNullException(nameof(configurationFileStorage));
        }

        /*
         * Возвращает список конфигураций (пока что только корневых - сборок верхнего уровня) по фильтру
         * todo Использовать Kendo UI
         * Используется как снаружи для получения и отображения списка конфигураций, так и фоновыми задачами
         */
        public async Task<ICollection<ConfigurationItemExDto>> GetConfigurationListAsync(ConfigurationFilterDto filter)
        {

            var items = await _dataAccessor.Reader.Configurations
                .Where(x => x.ParentConfigurationId == null && ((filter.Status != null && x.Status == filter.Status) || (filter.SvfStatus != null && x.SvfStatus == filter.SvfStatus)))
                .ProjectTo<ConfigurationItemExDto>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return items;
        }

        /*
         * Создаем заявку на конфигурацию. Т.е. создается конфигурация со статусом InQueue
         */
        public async Task<Guid> CreateConfigurationRequestAsync(ConfigurationRequestDto request)
        {
            var newConfiguration = _mapper.Map<Configuration>(request);
            newConfiguration.RootConfigurationId = newConfiguration.Id = Guid.NewGuid();
            newConfiguration.ComponentDefinitionId = (await _dataAccessor.Reader.Configurations.FirstOrDefaultAsync(x => x.Id == request.BaseConfigurationId)).ComponentDefinitionId;

            var parameters = await _dataAccessor.Reader.ParameterDefinitions.Where(x => x.ConfigurationId == request.BaseConfigurationId).ToListAsync();
            newConfiguration.ParameterDefinitions = new List<ParameterDefinition>();
            foreach (var parameter in parameters)
            {
                var newValue = request.ParameterValues.FirstOrDefault(x => x.ParameterDefinitionId == parameter.Id);
                if (newValue != null)
                    parameter.Value = newValue.Value;
                parameter.Id = Guid.NewGuid();
                newConfiguration.ParameterDefinitions.Add(parameter);
            }

            await _dataAccessor.Editor.CreateAsync(newConfiguration);
            await _dataAccessor.Editor.SaveAsync();
            //todo - if it find existed configuration, it will return a valid Guid
            return Guid.Empty;
        }

        /*
         * Создаем новую конфигурацию (и дочерние если есть) из пакета. Сразу присваиваем конфигурации статус Ready
         * Svf при этом у нас отсутствует и его нужно сформировать. Для этого присваивается соответствующий статус
         * todo - создание записи в БД и папки с файлами должно быть в рамках транзакции
         */
        public async Task CreateConfigurationFromPackageAsync(ConfigurationCreateDto create)
        {
            /*
             * Распаковываем пакет и кладем его в хранилище
             * Заранее формируем id конфигурации, т.к. хранилище должно его знать
             */
            var rootConfigurationId = Guid.NewGuid();
            var model = await _configurationFileStorage.SaveConfigurationPackageAsync(new ConfigurationPackageDto
            {
                ProductVersionId = create.ProductVersionId,
                ConfigurationId = rootConfigurationId,
                ConfigurationPackage = create.ConfigurationPackage.MapTo<FileStreamDto>(_mapper)
            });

            /* todo
             * Необходимо проверять, не существуют ли уже в БД ComponentDefinition, которые есть внутри нового Configuration
             * Проверять можно по UniqueId
             */
            var configurations = model.MapTo<ICollection<Configuration>>(_mapper);
            using (var ts = new TransactionScope())
            {
                foreach (var configuration in configurations)
                {
                    _mapper.Map(create, configuration);
                    _mapper.Map(create, configuration.ComponentDefinition);
                    if (configuration.ParentConfigurationId == null)
                    {
                        configuration.Id = rootConfigurationId;
                    }
                    configuration.RootConfigurationId = rootConfigurationId;
                    _dataAccessor.Editor.Create(configuration);
                }

                /* todo
                 * Добавить в базу эмэилы для уведомления подписчиков, когда будет сформирован svf
                 */

                _dataAccessor.Editor.Save();

                foreach (var configuration in configurations)
                {
                    configuration.TargetFileId = configuration.FileItems.First(x => x.FileId == configuration.TargetFileIdInternal).Id;
                }

                _dataAccessor.Editor.Save();

                ts.Complete();
            }
        }

        /*
         * Данный метод выполняется когда конфигурация перерасчитана и ее нужно сохранить
         * При этом учитываем, что запись корневой кофигурации была создана ранее как заявка
         * Вызывается фоновой задачей, когда получен ответ от инвентора
         */
        public async Task UpdateConfigurationAsync(Contracts.Dto.ConfigManager.ConfigurationStatusUpdateDto update)
        {
            /*
             * Получаем из базы корневую конфигурацию (заявку)
             */
            var rootConfiguration = await _dataAccessor.Editor.Configurations
                .Include(x => x.TemplateConfiguration)
                    .ThenInclude(x => x.ComponentDefinition)
                .FirstOrDefaultAsync(x => x.Id == update.ConfigurationId);
            if (rootConfiguration == null)
            {
                throw new EntityNotFoundException<Configuration>(update.ConfigurationId);
            }

            /*
             *  Распаковываем пакет и кладем его в хранилище
             */
            var model = await _configurationFileStorage.SaveConfigurationPackageAsync(new ConfigurationPackageDto
            {
                ProductVersionId = rootConfiguration.TemplateConfiguration.ComponentDefinition.ProductVersionId,
                ConfigurationId = update.ConfigurationId,
                ConfigurationPackage = update.ConfigurationPackage,
            });

            /*
             * todo Обновляем базу, обновляя корневую конфигурацию, добавляя дочерние кофигурации, 
             * добавляя ComponentDefinition (если такового нет), ParameterDefinition
             */

            var configurations = model.MapTo<ICollection<Configuration>>(_mapper);
            using (var ts = new TransactionScope())
            {
                foreach (var configuration in configurations)
                {
                    if (configuration.ParentConfigurationId == null)
                    {
                        _mapper.Map(configuration, rootConfiguration);
                    }
                    else
                    {
                        _dataAccessor.Editor.Create(configuration);
                    }
                }

                _dataAccessor.Editor.Save();

                foreach (var configuration in configurations)
                {
                    configuration.TargetFileId = configuration.FileItems.First(x => x.FileId == configuration.TargetFileIdInternal).Id;
                }

                _dataAccessor.Editor.Save();

                ts.Complete();
            }

            /*
             * todo Уведомляем подписчиков по email о том, что конфигурация перерасчитана
             */
        }

        /*
         * Имя файла (архива), содержащего все необходимые для инвентора данные
         * Вызывается фоновой задачей, которая делает отправку кофигурации на перерасчет в инвентор
         */
        public async Task<FileStreamDto> CreateConfigurationRequestPackageAsync(Guid configurationId)
        {
            /*
             * todo Поднять конфигурацию (запрос) с ее параметрами, поднять распакованный пакет из хранилища, 
             * заменить в json значения параметров, упаковать и вернуть архив как ответ             
             */
            var configuration = await _dataAccessor.Reader.Configurations.Include(x => x.ParameterDefinitions).FirstOrDefaultAsync(x => x.Id == configurationId);
            var tempConfiguration = await _dataAccessor.Reader.Configurations.Include(x => x.ComponentDefinition)
                .FirstOrDefaultAsync(x => x.Id == configuration.TemplateConfigurationId);
            var modelData = await _configurationFileStorage.GetPackageModelAsync(tempConfiguration.ComponentDefinition.ProductVersionId, tempConfiguration.Id);
            if (modelData == null)
                throw new EntityNotFoundException<ZipArchive>(tempConfiguration.Id);

            foreach (ParameterRow parameterRow in modelData.Parameter.Rows)
            {
                var newValue = configuration.ParameterDefinitions.FirstOrDefault(x => x.Name == parameterRow.Name);
                if (newValue != null)
                    parameterRow.Value = newValue.Value;
            }
            var json = JsonConvert.SerializeObject(modelData);

            _configurationFileStorage.CopyZipArchive(tempConfiguration.ComponentDefinition.ProductVersionId, tempConfiguration.Id, configurationId, json);
            return _configurationFileStorage.GetZipArchive(tempConfiguration.ComponentDefinition.ProductVersionId, configurationId);
        }

        /*
         * Возвращает дерево параметров конфигурации
         * Вызывается снаружи
         */
        public async Task<ConfigurationParametersDto> GetConfigurationParametersAsync(Guid configurationId)
        {
            var rootConfigurationId = (await _dataAccessor.Reader.Configurations.FirstOrDefaultAsync(x => x.Id == configurationId)).RootConfigurationId;
            var allConfigurations = await _dataAccessor.Reader.Configurations
                .Include(x => x.ComponentDefinition)
                .Include(x => x.ParameterDefinitions)
                .ThenInclude(x => x.ValueOptions)
                .Where(x => x.RootConfigurationId == rootConfigurationId).ToListAsync();

            return BuildTree(allConfigurations, configurationId);
        }

        private ConfigurationParametersDto BuildTree(ICollection<Configuration> allConfigurations, Guid id)
        {
            var configuration = allConfigurations.FirstOrDefault(x => x.Id == id);
            var result = new ConfigurationParametersDto()
            {
                ConfigurationId = configuration.Id,
                ConfigurationName = configuration.Name,
                ComponentName = configuration.ComponentDefinition.Name,
                Parameters = configuration.ParameterDefinitions.MapTo<ICollection<ParameterDefinitionDto>>(_mapper),
            };

            var childConfigurations = allConfigurations.Where(x => x.ParentConfigurationId == id).Select(x => x.Id).ToList();
            if (childConfigurations.Count > 0)
            {
                result.Childs = new List<ConfigurationParametersDto>();

                foreach (var childId in childConfigurations)
                    result.Childs.Add(BuildTree(allConfigurations, childId));
            }
            return result;
        }

        /*
         * Добавляет svf-файлы в хранилище и меняет статус svf конфигурации
         * Вызывается фоновой задачей, когда Forge API возвращает результат
         * Файлы добавляются в хранилище через IConfigurationFileStorage
         */
        public async Task AddSvfAsync(Guid configurationId, ICollection<Stream> svfList)
        {
            throw new NotImplementedException();
        }

        /*
         * Возвращает svf-файл для заданной конфигурации
         * Вызывается снаружи
         * svf-файлы получаем при помощи IConfigurationFileStorage
         */
        public async Task<FileStreamDto> GetSvfAsync(Guid configurationId, string svfName)
        {
            var productVersionId = await _dataAccessor.Reader.Configurations.Include(x => x.ComponentDefinition).
                Where(x => x.Id == configurationId).Select(x => x.ComponentDefinition.ProductVersionId).FirstOrDefaultAsync();

            return _configurationFileStorage.GetSvf(productVersionId, configurationId, svfName);
        }

        /*
         * Возвращает корневой svf-файл для заданной конфигурации
         * Имя svf-файла получаем при помощи IConfigurationFileStorage
         */
        public async Task<string> GetSvfRootFileNameAsync(Guid configurationId)
        {
            var productVersionId = await _dataAccessor.Reader.Configurations.Include(x => x.ComponentDefinition).
                Where(x => x.Id == configurationId).Select(x => x.ComponentDefinition.ProductVersionId).FirstOrDefaultAsync();

            return _configurationFileStorage.GetSvfRootFileName(productVersionId, configurationId);
        }

        public async Task<TResult> GetConfigurationItemsAsync<TResult>(Guid productVersionId, Func<IQueryable<ConfigurationItemDto>, TResult> resultBuilder)
        {
            if (resultBuilder == null)
            {
                throw new ArgumentNullException(nameof(resultBuilder));
            }
            var query = _dataAccessor.Reader.Configurations.Include(x => x.ComponentDefinition).
                Where(x => x.ComponentDefinition.ProductVersionId == productVersionId && x.ParentConfigurationId == null).
                ProjectTo<ConfigurationItemDto>(_mapper.ConfigurationProvider);
            var result = resultBuilder(query);
            return await Task.FromResult(result);
        }

        public void UpdateSvfStatus(ConfigurationSvfStatusUpdateDto update)
        {
            var item = _dataAccessor.Editor.Configurations.FirstOrDefault(x => x.Id == update.ConfigurationId);
            if (item == null)
            {
                throw new EntityNotFoundException<Configuration>(update.ConfigurationId);
            }

            _mapper.Map(update, item);
            _dataAccessor.Editor.Save();
        }

        public void UpdateModelStatus(ConfigurationUpdateModelDto update)
        {
            var item = _dataAccessor.Editor.Configurations.FirstOrDefault(x => x.Id == update.ConfigurationId);
            if (item == null)
            {
                throw new EntityNotFoundException<Configuration>(update.ConfigurationId);
            }

            _mapper.Map(update, item);
            _dataAccessor.Editor.Save();
        }

        public async Task<ConfigurationDto> GetConfigurationAsync(Guid id)
        {
            var result = await _dataAccessor.Reader.Configurations.ProjectTo<ConfigurationDto>(_mapper.ConfigurationProvider).FirstOrDefaultAsync(x => x.Id == id);
            if (result == null)
            {
                throw new EntityNotFoundException<Configuration>(id);
            }

            return result;
        }

        //public async Task<Guid> CreateConfigurationAsync(ConfigurationCreateDto create)
        //{
        //    if (create == null)
        //    {
        //        throw new ArgumentNullException(nameof(create));
        //    }

        //    /*var templateItem = await _dataAccessor.Reader.Configurations.
        //        Include(x => x.ParameterDefinitions).FirstOrDefaultAsync(x => x.Id == create.TemplateConfigurationId);
        //    //compare parameter definitions
        //    if (templateItem.ParameterDefinitions.Where(y => create.ParameterDefinitions.Any(z => z.DisplayName == y.DisplayName)).ToList().Count !=
        //        templateItem.ParameterDefinitions.Count)
        //    {
        //        //throw new OperationErrorException(0, "Incorrect parameter list");
        //}

        //    var newItem = _mapper.Map<Configuration>(create);
        //    _dataAccessor.Editor.Create(newItem);
        //    //await _dataAccessor.Editor.SaveAsync();
        //    var result1 = await _configManagerCommunicator.ProcessConfigurationAsync(newItem.Id);
        //    var result2 = await _configManagerCommunicator.GetSvfAsync(newItem.Id);
        //    return newItem.Id;*/
        //    return Guid.Empty;
    }

    /*public async Task UpdateConfigurationAsync(ConfigurationUpdateDto update)
    {
        if (update == null)
        {
            throw new ArgumentNullException(nameof(update));
    }

        var item = await _dataAccessor.Editor.Configurations.FirstOrDefaultAsync(x => x.Id == update.Id);
        if (item == null)
        {
            throw new EntityNotFoundException<Configuration>(update.Id);
        }

        _mapper.Map(update, item);
        await _dataAccessor.Editor.SaveAsync();
    }

    public async Task RemoveConfigurationAsync(Guid id)
    {
        var item = await _dataAccessor.Editor.Configurations.FirstOrDefaultAsync(x => x.Id == id);
        if (item == null)
        {
            throw new EntityNotFoundException<Configuration>(id);
        }

        _dataAccessor.Editor.Delete(item);
        DeleteFiles(item.ProductVersionId, item.Id);
        await _dataAccessor.Editor.SaveAsync();
    }

    private string GetModelFileName(Guid productVersionId, Guid id)
    {
        var filePath = $"{_fileBucket}{productVersionId}\\{id}\\";
        var di = new DirectoryInfo(filePath);
        return di.Exists ? di.EnumerateFiles().FirstOrDefault()?.Name ?? string.Empty : string.Empty;
    }

    public async Task<AttachmentDto> GetModelFileAsync(Guid id)
    {
        var filePath = $"{_fileBucket}{id}\\model\\";
        var di = new DirectoryInfo(filePath);
        if (di.Exists)
        {
            var file = di.EnumerateFiles().FirstOrDefault();
            if (file != null)
                return await GetFileAsync(file);
    }

        return null;
    }

    private async Task<AttachmentDto> GetFileAsync(FileInfo file)
    {
        var result = new AttachmentDto
        {
            FileName = file.Name,
            Content = await File.ReadAllBytesAsync(file.FullName),
            ContentType = "application/octet-stream"
        };
        result.Length = result.Content.Length;
        return result;
    }

    private void DeleteFiles(Guid productVersionId, Guid id)
    {
        var filePath = $"{_fileBucket}{productVersionId}\\{id}";
        var di = new DirectoryInfo(filePath);
        if (di.Exists)
            di.Delete(true);
    }*/
}
