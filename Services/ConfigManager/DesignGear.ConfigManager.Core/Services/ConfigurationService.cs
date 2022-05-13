﻿using DesignGear.Contracts.Dto;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using DesignGear.Common.Exceptions;
using DesignGear.Contracts.Communicators.Interfaces;
using DesignGear.ConfigManager.Core.Services.Interfaces;
using DesignGear.ConfigManager.Core.Data;
using DesignGear.Contracts.Dto.ConfigManager;

namespace DesignGear.ConfigManager.Core.Services
{
    public class ConfigurationService : IConfigurationService
    {
        private readonly IMapper _mapper;
        private readonly DataAccessor _dataAccessor;
        private readonly string _fileBucket = @"C:\DesignGearFiles\Versions\";

        public ConfigurationService(IMapper mapper, DataAccessor dataAccessor)
        {
            _mapper = mapper;
            _dataAccessor = dataAccessor;
            
        }

        public async Task<ICollection<ConfigurationItemDto>> GetConfigurationList() {
            var items = await _dataAccessor.Reader.Configurations
                .Where(x => x.ComponentDefinition.ParentComponentDefinitionId == null)
                .ProjectTo<ConfigurationItemDto>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return items;
        }

        /*
         * Создаем новую конфигурацию и присваиваем конфигурации статус InQueue
         */
        public async Task CreateConfigurationRequestAsync(ConfigurationRequestDto requst) {

        }

        /*
         * Создаем новую конфигурацию (и если есть дочерние организации), распарсив json. Присваиваем конфигурации статус Ready
         */
        public async Task CreateConfigurationAsync(ConfigurationCreateDto create) {

        }

        /*
         * Имя файла (архива), содержащего все необходимые для инвентора данные
         */
        public async Task<string> CreateConfigurationRequestPackage(Guid configurationId) {
            return "";
        }

        public async Task AddSvfAsync() {

        }

        /*
         * Возвращает набор svf?
         */
        public async Task<Stream> GetSvfAsync(Guid configurationId) {
            return null;
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

        public async Task<ICollection<ConfigurationItemDto>> GetConfigurationItemsAsync(Guid productVersionId)
        {
            return await _dataAccessor.Reader.Configurations.Where(x => x.ProductVersionId == productVersionId).
                ProjectTo<ConfigurationItemDto>(_mapper.ConfigurationProvider).ToListAsync();
        }

        public async Task<ConfigurationDto> GetConfigurationAsync(Guid id)
        {
            var result = await _dataAccessor.Reader.Configurations.ProjectTo<ConfigurationDto>(_mapper.ConfigurationProvider).FirstOrDefaultAsync(x => x.Id == id);
            if (result == null)
            {
                throw new EntityNotFoundException<Configuration>(id);
        }

            result.ModelFile = GetModelFileName(result.ProductVersionId, id);

            return result;
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