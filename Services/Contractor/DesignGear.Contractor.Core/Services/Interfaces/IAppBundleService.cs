using DesignGear.Contracts.Dto;
using Kendo.Mvc.UI;

namespace DesignGear.Contractor.Core.Services.Interfaces
{
    public interface IAppBundleService
    {
        Task<DataSourceResult> GetAppBundlesAsync(string queryString);

        Task<Guid> CreateAppBundleAsync(CreateAppBundleDto create);

        Task UpdateAppBundleAsync(UpdateAppBundleDto update);

        Task RemoveAppBundleAsync(Guid id);

        Task<AppBundleDto> GetAppBundleAsync(Guid id);
    }
}
