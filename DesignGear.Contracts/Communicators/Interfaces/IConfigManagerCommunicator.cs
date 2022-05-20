using DesignGear.Contracts.Dto;
using DesignGear.Contracts.Models.ConfigManager;

namespace DesignGear.Contracts.Communicators.Interfaces
{
    public interface IConfigManagerCommunicator
    {
        Task<string> ProcessConfigurationAsync(Guid id);

        Task<string> GetSvfAsync(Guid id);

        Task<ICollection<AppBundleDto>> GetAppBundleListAsync();
        
        Task CreateConfigurationAsync(VmConfigurationCreate create);

        Task CreateConfigurationRequestAsync(VmConfigurationRequest request);

        Task UpdateConfigurationAsync(ConfigurationUpdateDto update);

        Task<FileStreamDto> GetSvfAsync(Guid configurationId, string svfName);

        Task<string> GetSvfRootFileNameAsync(Guid configurationId);

        Task<Dto.ConfigManager.ConfigurationParametersDto> GetConfigurationParametersAsync(Guid configurationId);
    }
}
