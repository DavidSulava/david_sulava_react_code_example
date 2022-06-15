using DesignGear.Contracts.Dto;

namespace DesignGear.ConfigManager.Core.Services.Interfaces
{
    public interface IAppBundleService
    {
        Task<TResult> GetAppBundleListAsync<TResult>(Func<IQueryable<AppBundleDto>, TResult> resultBuilder);

        Task<Guid> CreateAppBundleAsync(CreateAppBundleDto create);

        Task UpdateAppBundleAsync(UpdateAppBundleDto update);

        Task RemoveAppBundleAsync(Guid id);

        Task<AppBundleDto> GetAppBundleAsync(Guid id);
    }
}
