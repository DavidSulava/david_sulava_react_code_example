using DesignGear.Contracts.Dto;
using DesignGear.Contracts.Models.ConfigManager;
using Kendo.Mvc.UI;

namespace DesignGear.Contracts.Communicators.Interfaces
{
    public interface IConfigManagerCommunicator
    {
        Task<ICollection<AppBundleDto>> GetAppBundleListAsync();
        
        Task CreateConfigurationAsync(VmConfigurationCreate create);

        Task CreateConfigurationRequestAsync(VmConfigurationRequest request);

        Task UpdateConfigurationAsync(ConfigurationUpdateDto update);

        Task<FileStreamDto> GetSvfAsync(Guid configurationId, string svfName);

        Task<string> GetSvfRootFileNameAsync(Guid configurationId);

        Task<Dto.ConfigManager.ConfigurationParametersDto> GetConfigurationParametersAsync(Guid configurationId);

        Task<DataSourceResult> GetConfigurationItemsAsync(string queryString);
    }
}
