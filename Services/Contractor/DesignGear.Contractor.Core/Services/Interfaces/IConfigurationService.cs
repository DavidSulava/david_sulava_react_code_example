using DesignGear.Contracts.Dto;
using DesignGear.Contracts.Dto.ConfigManager;
using DesignGear.Contracts.Models.ConfigManager;
using Kendo.Mvc.UI;

namespace DesignGear.Contractor.Core.Services.Interfaces
{
    public interface IConfigurationService
    {
        Task<Guid> CreateConfigurationRequestAsync(VmConfigurationRequest request);

        //Task UpdateConfigurationAsync(Contracts.Dto.ConfigurationUpdateDto Configuration);

        //Task RemoveConfigurationAsync(Guid id);

        //Task<ICollection<ConfigurationItemDto>> GetConfigurationItemsAsync(Guid productVersionId);

        Task<DataSourceResult> GetConfigurationItemsAsync(string queryString);

        Task<ConfigurationDto> GetConfigurationAsync(Guid id);

        //Task<AttachmentDto> GetModelFileAsync(Guid id);

        Task<FileStreamDto> GetSvfAsync(Guid configurationId, string svfName);

        Task<string> GetSvfRootFileNameAsync(Guid configurationId);

        Task<ConfigurationParametersDto> GetConfigurationParametersAsync(Guid configurationId);
    }
}
