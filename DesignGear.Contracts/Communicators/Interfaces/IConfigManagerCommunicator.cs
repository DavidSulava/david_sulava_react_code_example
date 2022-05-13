using DesignGear.Contracts.Dto;

namespace DesignGear.Contracts.Communicators.Interfaces
{
    public interface IConfigManagerCommunicator
    {
        Task<string> ProcessConfigurationAsync(Guid id);

        Task<string> GetSvfAsync(Guid id);

        Task<ICollection<AppBundleDto>> GetAppBundleListAsync();
        //Task CreateConfigurationAsync(CreateConfigurationRequest create);
    }
}
