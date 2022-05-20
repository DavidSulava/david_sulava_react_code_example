using DesignGear.Contracts.Dto;
using DesignGear.Contracts.Dto.ConfigManager;
using DesignGear.Contracts.Models.ConfigManager;

namespace DesignGear.Contractor.Core.Services.Interfaces
{
    public interface IConfigurationService
    {
        Task CreateConfigurationRequestAsync(VmConfigurationRequest request);

        //Task UpdateConfigurationAsync(Contracts.Dto.ConfigurationUpdateDto Configuration);

        //Task RemoveConfigurationAsync(Guid id);

        //Task<ICollection<ConfigurationItemDto>> GetConfigurationItemsAsync(Guid productVersionId);
        //Task<TResult> GetConfigurationItemsAsync<TResult>(Guid productVersionId, Func<IQueryable<ConfigurationDto>, TResult> resultBuilder);

        //Task<ConfigurationDto> GetConfigurationAsync(Guid id);

        //Task<AttachmentDto> GetModelFileAsync(Guid id);

        Task<FileStreamDto> GetSvfAsync(Guid configurationId, string svfName);

        Task<string> GetSvfRootFileNameAsync(Guid configurationId);

        Task<ConfigurationParametersDto> GetConfigurationParametersAsync(Guid configurationId);
    }
}
