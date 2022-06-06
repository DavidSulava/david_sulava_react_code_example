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
    public class AppBundleController : ControllerBase
    {
        private readonly IAppBundleService _appBundleService;
        private readonly IMapper _mapper;

        public AppBundleController(IAppBundleService appBundleService, IMapper mapper)
        {
            _appBundleService = appBundleService ?? throw new ArgumentNullException(nameof(appBundleService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpPost]
        public async Task<Guid> CreateAppBundleAsync([FromForm] VmAppBundleCreate create)
        {
            return await _appBundleService.CreateAppBundleAsync(create.MapTo<CreateAppBundleDto>(_mapper));
        }

        [HttpPut]
        public async Task UpdateAppBundleAsync([FromForm] VmAppBundleUpdate update)
        {
            await _appBundleService.UpdateAppBundleAsync(update.MapTo<UpdateAppBundleDto>(_mapper));
        }

        [HttpDelete]
        public async Task RemoveAppBundleAsync(Guid id)
        {
            await _appBundleService.RemoveAppBundleAsync(id);
        }

        [HttpGet]
        public async Task<ICollection<VmAppBundleItem>> GetAppBundleListAsync()
        {
            return (await _appBundleService.GetAppBundleListAsync()).MapTo<ICollection<VmAppBundleItem>>(_mapper);
        }

        [HttpGet("{id}")]
        public async Task<VmAppBundleItem> GetAppBundleAsync([FromRoute] Guid id)
        {
            return (await _appBundleService.GetAppBundleAsync(id)).MapTo<VmAppBundleItem>(_mapper);
        }
    }
}
