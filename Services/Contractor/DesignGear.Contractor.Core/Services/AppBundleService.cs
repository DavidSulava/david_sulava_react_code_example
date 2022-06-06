using AutoMapper;
using AutoMapper.QueryableExtensions;
using DesignGear.Contractor.Core.Data;
using DesignGear.Contractor.Core.Services.Interfaces;
using DesignGear.Contracts.Communicators.Interfaces;
using DesignGear.Contracts.Dto;
using Microsoft.EntityFrameworkCore;

namespace DesignGear.Contractor.Core.Services
{
    public class AppBundleService : IAppBundleService
    {
        private readonly IMapper _mapper;
        private readonly DataAccessor _dataAccessor;
        private readonly IConfigManagerCommunicator _configManagerService;

        public AppBundleService(IMapper mapper, DataAccessor dataAccessor, IConfigManagerCommunicator configManagerService)
        {
            _mapper = mapper;
            _dataAccessor = dataAccessor;
            _configManagerService = configManagerService;
        }

        public async Task<Guid> CreateAppBundleAsync(CreateAppBundleDto create)
        {
            return await _configManagerService.CreateAppBundleAsync(create);
        }

        public async Task UpdateAppBundleAsync(UpdateAppBundleDto update)
        {
            await _configManagerService.UpdateAppBundleAsync(update);
        }

        public async Task RemoveAppBundleAsync(Guid id)
        {
            await _configManagerService.RemoveAppBundleAsync(id);
        }

        public async Task<ICollection<AppBundleDto>> GetAppBundlesAsync()
        {
            return await _configManagerService.GetAppBundleListAsync();
        }

        public async Task<AppBundleDto> GetAppBundleAsync(Guid id)
        {
            return await _configManagerService.GetAppBundleAsync(id);
        }
    }
}
