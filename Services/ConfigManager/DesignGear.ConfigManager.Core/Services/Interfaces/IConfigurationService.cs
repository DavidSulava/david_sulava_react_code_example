using DesignGear.Contracts.Dto.ConfigManager;

namespace DesignGear.ConfigManager.Core.Services.Interfaces
{
    public interface IConfigurationService
    {
        Task<ICollection<ConfigurationItemDto>> GetConfigurationListAsync(ConfigurationFilterDto filter);
        Task CreateConfigurationRequestAsync(ConfigurationRequestDto request);
        Task CreateConfigurationFromPackageAsync(ConfigurationCreateDto create);
        Task<Stream> CreateConfigurationRequestPackageAsync(Guid configurationId);
        Task AddSvfAsync(Guid configurationId, ICollection<Stream> svfList);
        Task<Stream> GetSvfAsync(Guid configurationId);



        /*Task UpdateConfigurationAsync(ConfigurationUpdateDto Configuration);

        Task RemoveConfigurationAsync(Guid id);

        Task<ICollection<ConfigurationItemDto>> GetConfigurationItemsAsync(Guid productVersionId);

        Task<ConfigurationDto> GetConfigurationAsync(Guid id);

        Task<AttachmentDto> GetModelFileAsync(Guid id);*/
    }
}
