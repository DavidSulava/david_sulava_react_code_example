using DesignGear.Contractor.Core.Data;
using DesignGear.Contracts.Dto;
using DesignGear.Contractor.Core.Services.Interfaces;
using DesignGear.Contractor.Core.Data.Entity;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using DesignGear.Common.Exceptions;

namespace DesignGear.Contractor.Core.Services
{
    public class ConfigurationService : IConfigurationService
    {
        private readonly IMapper _mapper;
        private readonly DataAccessor _dataAccessor;

        public ConfigurationService(IMapper mapper, DataAccessor dataAccessor)
        {
            _mapper = mapper;
            _dataAccessor = dataAccessor;
        }

        public async Task<Guid> CreateConfigurationAsync(ConfigurationCreateDto create)
        {
            if (create == null)
            {
                throw new ArgumentNullException(nameof(create));
            }

            var templateItem = await _dataAccessor.Reader.Configurations.FirstOrDefaultAsync(x => x.Id == create.TemplateConfigurationId);
            //compare parameter definitions
            if (templateItem.ParameterDefinitions.Where(y => create.ParameterDefinitions.Any(z => z.DisplayName == y.DisplayName)).ToList().Count !=
                templateItem.ParameterDefinitions.Count)
            {
                throw new OperationErrorException(0, "Incorrect parameter list");
            }

            var newItem = _mapper.Map<Configuration>(create);
            _dataAccessor.Editor.Create(newItem);
            await _dataAccessor.Editor.SaveAsync();
            return newItem.Id;
        }

        public async Task UpdateConfigurationAsync(ConfigurationUpdateDto update)
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
            await _dataAccessor.Editor.SaveAsync();
        }

        public async Task<ICollection<ConfigurationDto>> GetConfigurationsByProductVersionAsync(Guid productVersionId)
        {
            return await _dataAccessor.Reader.Configurations.Where(x => x.ProductVersionId == productVersionId).
                ProjectTo<ConfigurationDto>(_mapper.ConfigurationProvider).ToListAsync();
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
    }
}
