using DesignGear.Contracts.Dto;
using DesignGear.Contracts.Models.ConfigManager;

namespace DesignGear.Contractor.Core.Services.Interfaces
{
    public interface IConfigurationService
    {
        Task CreateConfigurationAsync(VmConfigurationRequest request);

        Task UpdateConfigurationAsync(ConfigurationUpdateDto Configuration);

        Task RemoveConfigurationAsync(Guid id);

        //Task<ICollection<ConfigurationItemDto>> GetConfigurationItemsAsync(Guid productVersionId);

        Task<ConfigurationDto> GetConfigurationAsync(Guid id);

        Task<AttachmentDto> GetModelFileAsync(Guid id);
    }
}
