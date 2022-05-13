using AutoMapper;
using AutoMapper.QueryableExtensions;
using DesignGear.ConfigManager.Core.Data;
using DesignGear.ConfigManager.Core.Data.Entity;
using DesignGear.ConfigManager.Core.Services.Interfaces;
using DesignGear.Contracts.Dto;
using Microsoft.EntityFrameworkCore;

namespace DesignGear.ConfigManager.Core.Services
{
    public class AppBundleService : IAppBundleService
    {
        private readonly IMapper _mapper;
        private readonly DataAccessor _dataAccessor;

        public AppBundleService(IMapper mapper, DataAccessor dataAccessor)
        {
            _mapper = mapper;
            _dataAccessor = dataAccessor;
        }

        public async Task<Guid> CreateAppBundleAsync(CreateAppBundleDto create)
        {
            if (create == null)
            {
                throw new ArgumentNullException(nameof(create));
            }

            var newItem = _mapper.Map<AppBundle>(create);
            _dataAccessor.Editor.Create(newItem);
            await _dataAccessor.Editor.SaveAsync();
            return newItem.Id;
        }

        public async Task<ICollection<AppBundleDto>> GetAppBundlesAsync()
        {
            return await _dataAccessor.Reader.AppBundles.ProjectTo<AppBundleDto>(_mapper.ConfigurationProvider).ToListAsync();
        }
    }
}
