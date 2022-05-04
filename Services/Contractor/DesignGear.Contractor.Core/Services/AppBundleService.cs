using AutoMapper;
using AutoMapper.QueryableExtensions;
using DesignGear.Contractor.Core.Data;
using DesignGear.Contractor.Core.Services.Interfaces;
using DesignGear.Contracts.Dto;
using Microsoft.EntityFrameworkCore;

namespace DesignGear.Contractor.Core.Services
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

        public async Task<ICollection<AppBundleDto>> GetAppBundlesAsync()
        {
            return await _dataAccessor.Reader.AppBundles.ProjectTo<AppBundleDto>(_mapper.ConfigurationProvider).ToListAsync();
        }
    }
}
