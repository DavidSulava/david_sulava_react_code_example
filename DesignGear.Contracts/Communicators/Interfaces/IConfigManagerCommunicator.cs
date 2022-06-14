using DesignGear.Contracts.Dto;
using DesignGear.Contracts.Dto.ConfigManager;
using DesignGear.Contracts.Models.ConfigManager;
using Kendo.Mvc.UI;

namespace DesignGear.Contracts.Communicators.Interfaces
{
    public interface IConfigManagerCommunicator
    {
        Task<Guid> CreateAppBundleAsync(CreateAppBundleDto create);

        Task UpdateAppBundleAsync(UpdateAppBundleDto update);
        
        Task RemoveAppBundleAsync(Guid id);

        Task<ICollection<AppBundleDto>> GetAppBundleListAsync();

        Task<AppBundleDto> GetAppBundleAsync(Guid id);


        Task CreateConfigurationAsync(VmConfigurationCreate create);

        Task<Guid> CreateConfigurationRequestAsync(VmConfigurationRequest request);

        //Task UpdateConfigurationAsync(ConfigurationUpdateDto update);

        Task<Stream> GetSvfAsync(Guid configurationId, string svfName);

        Task<string> GetSvfRootFileNameAsync(Guid configurationId);

        Task<ConfigurationParametersDto> GetConfigurationParametersAsync(Guid configurationId);

        Task<DataSourceResult> GetConfigurationItemsAsync(string queryString);

        Task<ConfigurationDto> GetConfigurationAsync(Guid id);

        Task RemoveConfigurationAsync(Guid id);
    }
}
