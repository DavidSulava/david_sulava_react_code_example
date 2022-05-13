using AutoMapper;
using DesignGear.ConfigManager.Core.Services.Interfaces;
using DesignGear.Contracts.Models.ConfigManager;
using Microsoft.AspNetCore.Mvc;
using DesignGear.Common.Extensions;
using DesignGear.Contracts.Dto;

namespace DesignGear.ConfigManager.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AppBundleController : ControllerBase {
        private readonly IAppBundleService _appBundleService;
        private readonly IMapper _mapper;

        public AppBundleController(IAppBundleService appBundleService, IMapper mapper)
        {
            _appBundleService = appBundleService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<Guid> CreateAppBundleAsync(VmAppBundleCreate create) {
            return await _appBundleService.CreateAppBundleAsync(create.MapTo<CreateAppBundleDto>(_mapper));
        }

        [HttpGet]
        public async Task<ICollection<VmAppBundleItem>> GetAppBundleListAsync() {
            return(await _appBundleService.GetAppBundlesAsync()).MapTo<ICollection<VmAppBundleItem>>(_mapper);
        }
    }
}
