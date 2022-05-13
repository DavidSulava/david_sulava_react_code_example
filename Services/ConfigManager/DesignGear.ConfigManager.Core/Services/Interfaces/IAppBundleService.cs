using DesignGear.Contracts.Dto;

namespace DesignGear.ConfigManager.Core.Services.Interfaces
{
    public interface IAppBundleService
    {
        Task<ICollection<AppBundleDto>> GetAppBundlesAsync();

        Task<Guid> CreateAppBundleAsync(CreateAppBundleDto create);
    }
}
