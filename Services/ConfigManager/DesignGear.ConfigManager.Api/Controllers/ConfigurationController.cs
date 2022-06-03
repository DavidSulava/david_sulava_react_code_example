using Microsoft.AspNetCore.Mvc;
using DesignGear.Contracts.Models.ConfigManager;
using DesignGear.ConfigManager.Core.Services.Interfaces;
using AutoMapper;
using DesignGear.Contracts.Dto;
using DesignGear.Contracts.Dto.ConfigManager;
using DesignGear.Common.Extensions;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using Newtonsoft.Json;

namespace DesignGear.ConfigManager.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ConfigurationController : ControllerBase
    {
        private readonly IConfigurationService _configurationService;
        private readonly IMapper _mapper;

        public ConfigurationController(IConfigurationService configurationService, IMapper mapper)
        {
            _configurationService = configurationService;
            _mapper = mapper;
        }


        [HttpPost]
        public async Task CreateConfigurationAsync([FromForm] VmConfigurationCreate create)
        {
            await _configurationService.CreateConfigurationFromPackageAsync(create.MapTo<ConfigurationCreateDto>(_mapper));
        }

        [HttpPost("request")]
        public async Task CreateConfigurationRequestAsync([FromBody] VmConfigurationRequest request)
        {
            await _configurationService.CreateConfigurationRequestAsync(request.MapTo<ConfigurationRequestDto>(_mapper));
        }

        [HttpGet]
        public async Task<IActionResult> GetConfigurationItemsAsync(Guid productVersionId, [DataSourceRequest] DataSourceRequest dataSourceRequest)
        {
            var result = await _configurationService.GetConfigurationItemsAsync(productVersionId, query => query.ToDataSourceResult(dataSourceRequest, _mapper.Map<ConfigurationItemDto, VmConfigurationItem>));
            var json = JsonConvert.SerializeObject(result, new JsonSerializerSettings() { TypeNameHandling = TypeNameHandling.All });
            return Ok(json);
        }

        [HttpGet("{configurationId}/svf")]
        public async Task<string> GetSvfRootFileNameAsync([FromRoute] Guid configurationId)
        {
            return await _configurationService.GetSvfRootFileNameAsync(configurationId);
        }

        [HttpGet("{configurationId}/svf/{*svfName}")]
        public async Task<IActionResult> GetSvfAsync([FromRoute] Guid configurationId, [FromRoute] string svfName)
        {
            var result = await _configurationService.GetSvfAsync(configurationId, svfName);
            if(result != null)
                return File(result.Content, result.ContentType, result.FileName);
            return Ok();
        }

        [HttpGet("{configurationId}/parameters")]
        public async Task<VmComponentParameterDefinitions> GetConfigurationParameterDefinitionsAsync([FromRoute] Guid configurationId)
        {
            return (await _configurationService.GetConfigurationParametersAsync(configurationId)).MapTo<VmComponentParameterDefinitions>(_mapper);
        }

        //[HttpGet("{id}")]
        //public async Task<VmConfiguration> GetConfigurationAsync([FromRoute] Guid id) {
        //    return null;
        //}

        //[HttpDelete("{id}")]
        //public async Task DeleteConfigurationAsync([FromRoute] Guid id) {

        //}
    }
}
