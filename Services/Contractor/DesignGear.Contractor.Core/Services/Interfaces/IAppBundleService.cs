using DesignGear.Contracts.Dto;

namespace DesignGear.Contractor.Core.Services.Interfaces
{
    public interface IAppBundleService
    {
        Task<ICollection<AppBundleDto>> GetAppBundlesAsync();

        Task<Guid> CreateAppBundleAsync(CreateAppBundleDto create);

        Task UpdateAppBundleAsync(UpdateAppBundleDto update);

        Task RemoveAppBundleAsync(Guid id);

        Task<AppBundleDto> GetAppBundleAsync(Guid id);
    }
}
