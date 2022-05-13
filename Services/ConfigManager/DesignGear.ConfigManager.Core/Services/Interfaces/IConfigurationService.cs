using DesignGear.Contracts.Dto.ConfigManager;

namespace DesignGear.ConfigManager.Core.Services.Interfaces
{
    public interface IConfigurationService
    {
        Task<ICollection<ConfigurationItemDto>> GetConfigurationList();
        Task CreateConfigurationRequestAsync(ConfigurationRequestDto request);
        Task CreateConfigurationAsync(ConfigurationCreateDto create);
        Task<string> CreateConfigurationRequestPackage(Guid configurationId);
        Task AddSvfAsync();
        Task<Stream> GetSvfAsync(Guid configurationId);



        /*Task UpdateConfigurationAsync(ConfigurationUpdateDto Configuration);

        Task RemoveConfigurationAsync(Guid id);

        Task<ICollection<ConfigurationItemDto>> GetConfigurationItemsAsync(Guid productVersionId);

        Task<ConfigurationDto> GetConfigurationAsync(Guid id);

        Task<AttachmentDto> GetModelFileAsync(Guid id);*/
    }
}
