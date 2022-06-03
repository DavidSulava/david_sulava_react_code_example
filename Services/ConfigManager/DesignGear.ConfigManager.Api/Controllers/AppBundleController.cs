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

        //[HttpPut]
        //public async Task UpdateAppBundleAsync(VmAppBundleUpdate appBundle)
        //{
        //    await _appBundleService.UpdateAppBundleAsync(appBundle.MapTo<UpdateAppBundleDto>(_mapper));
        //}

        //[HttpDelete]
        //public async Task RemoveAppBundleAsync(Guid appBundleId)
        //{
        //    await _appBundleService.RemoveAppBundleAsync(appBundleId);
        //}

        [HttpGet]
        public async Task<ICollection<VmAppBundleItem>> GetAppBundleListAsync() {
            return(await _appBundleService.GetAppBundlesAsync()).MapTo<ICollection<VmAppBundleItem>>(_mapper);
        }

        //[HttpGet("{id}")]
        //public async Task<VmAppBundle> GetAppBundleAsync([FromRoute] Guid id)
        //{
        //    return (await _appBundleService.GetAppBundleAsync(id)).MapTo<VmAppBundle>(_mapper);
        //}
    }
}
