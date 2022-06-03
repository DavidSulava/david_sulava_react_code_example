using AutoMapper;
using AutoMapper.QueryableExtensions;
using DesignGear.Common.Exceptions;
using DesignGear.ConfigManager.Core.Data;
using DesignGear.ConfigManager.Core.Data.Entity;
using DesignGear.ConfigManager.Core.Services.Interfaces;
using DesignGear.Contracts.Dto;
using DesignGear.Contracts.Models.Contractor;
using Microsoft.EntityFrameworkCore;

namespace DesignGear.ConfigManager.Core.Services
{
    public class AppBundleService : IAppBundleService
    {
        private readonly IMapper _mapper;
        private readonly DataAccessor _dataAccessor;

        public AppBundleService(IMapper mapper, DataAccessor dataAccessor)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _dataAccessor = dataAccessor ?? throw new ArgumentNullException(nameof(dataAccessor));
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

        public async Task UpdateAppBundleAsync(UpdateAppBundleDto update)
        {
            if (update == null)
            {
                throw new ArgumentNullException(nameof(update));
            }

            var item = await _dataAccessor.Editor.AppBundles.FirstOrDefaultAsync(x => x.Id == update.Id);
            if (item == null)
            {
                throw new EntityNotFoundException<AppBundle>(update.Id);
            }

            _mapper.Map(update, item);
            await _dataAccessor.Editor.SaveAsync();
        }

        public async Task RemoveAppBundleAsync(Guid id)
        {
            var item = await _dataAccessor.Editor.AppBundles.FirstOrDefaultAsync(x => x.Id == id);
            if (item == null)
            {
                throw new EntityNotFoundException<AppBundle>(id);
            }

            _dataAccessor.Editor.Delete(item);
            await _dataAccessor.Editor.SaveAsync();
        }

        public async Task<ICollection<AppBundleDto>> GetAppBundleListAsync()
        {
            return await _dataAccessor.Reader.AppBundles.ProjectTo<AppBundleDto>(_mapper.ConfigurationProvider).ToListAsync();
        }

        public async Task<AppBundleDto> GetAppBundleAsync(Guid id)
        {
            var result = await _dataAccessor.Reader.AppBundles.ProjectTo<AppBundleDto>(_mapper.ConfigurationProvider).FirstOrDefaultAsync(x => x.Id == id);
            if (result == null)
            {
                throw new EntityNotFoundException<AppBundle>(id);
            }

            return result;
        }
    }
}
