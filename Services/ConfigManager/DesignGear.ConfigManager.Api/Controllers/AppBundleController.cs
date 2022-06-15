using AutoMapper;
using DesignGear.ConfigManager.Core.Services.Interfaces;
using DesignGear.Contracts.Models.ConfigManager;
using Microsoft.AspNetCore.Mvc;
using DesignGear.Common.Extensions;
using DesignGear.Contracts.Dto;
using Kendo.Mvc.UI;
using Newtonsoft.Json;
using Kendo.Mvc.Extensions;

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

        [HttpDelete("{id}")]
        public async Task RemoveAppBundleAsync([FromRoute] Guid id)
        {
            await _appBundleService.RemoveAppBundleAsync(id);
        }

        [HttpGet]
        public async Task<IActionResult> GetAppBundleListAsync([DataSourceRequest] DataSourceRequest dataSourceRequest)
        {
            var result = await _appBundleService.GetAppBundleListAsync(query => query.ToDataSourceResult(dataSourceRequest, _mapper.Map<AppBundleDto, VmAppBundleItem>));
            var json = JsonConvert.SerializeObject(result, new JsonSerializerSettings() { TypeNameHandling = TypeNameHandling.All });
            return Ok(json);
        }

        [HttpGet("{id}")]
        public async Task<VmAppBundleItem> GetAppBundleAsync([FromRoute] Guid id)
        {
            return (await _appBundleService.GetAppBundleAsync(id)).MapTo<VmAppBundleItem>(_mapper);
        }
    }
}
