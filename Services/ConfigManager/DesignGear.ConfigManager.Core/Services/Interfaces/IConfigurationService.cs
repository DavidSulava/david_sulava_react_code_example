using DesignGear.Contracts.Dto;
using DesignGear.Contracts.Dto.ConfigManager;
using Kendo.Mvc.UI;

namespace DesignGear.ConfigManager.Core.Services.Interfaces
{
    public interface IConfigurationService
    {
        Task<ICollection<ConfigurationItemExDto>> GetConfigurationListAsync(ConfigurationFilterDto filter);
        Task<Guid> CreateConfigurationRequestAsync(ConfigurationRequestDto request);
        Task CreateConfigurationFromPackageAsync(ConfigurationCreateDto create);
        Task<FileStreamDto> CreateConfigurationRequestPackageAsync(Guid configurationId);
        Task AddSvfAsync(Guid configurationId, ICollection<Stream> svfList);
        //Task<Stream> GetSvfAsync(Guid configurationId);

        Task<TResult> GetConfigurationItemsAsync<TResult>(Guid productVersionId, Func<IQueryable<ConfigurationItemDto>, TResult> resultBuilder);

        Task<FileStreamDto> GetSvfAsync(Guid configurationId, string svfName);

        Task<string> GetSvfRootFileNameAsync(Guid configurationId);

        Task<ConfigurationParametersDto> GetConfigurationParametersAsync(Guid configurationId);

        void UpdateSvfStatus(ConfigurationUpdateSvfDto update);

        void UpdateModelStatus(ConfigurationUpdateModelDto update);

        Task UpdateConfigurationAsync(Contracts.Dto.ConfigManager.ConfigurationUpdateDto Configuration);

        /*Task RemoveConfigurationAsync(Guid id);

        Task<ICollection<ConfigurationItemDto>> GetConfigurationItemsAsync(Guid productVersionId);

        Task<ConfigurationDto> GetConfigurationAsync(Guid id);

        Task<AttachmentDto> GetModelFileAsync(Guid id);*/
    }
}
