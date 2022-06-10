using AutoMapper;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;
using DesignGear.Common.Extensions;
using DesignGear.Contracts.Models.ConfigManager;
using DesignGear.Contractor.Core.Helpers;
using DesignGear.Contractor.Core.Services.Interfaces;

namespace DesignGear.Contractor.Api.Controllers
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

        [Authorize(Policy = "OrganizationSelected")]
        [HttpPost]
        public async Task<IActionResult> CreateConfigurationRequestAsync([FromBody] VmConfigurationRequest request)
        {
            var result = await _configurationService.CreateConfigurationRequestAsync(request);
            if (result == Guid.Empty)
                return Ok();
            else
                return Ok(result);
        }

        [Authorize(Policy = "OrganizationSelected")]
        [HttpGet]
        public async Task<DataSourceResult> GetConfigurationItemsAsync(Guid productVersionId, [DataSourceRequest] DataSourceRequest dataSourceRequest)
        {
            var queryString = this.Request.QueryString.Value;
            return await _configurationService.GetConfigurationItemsAsync(queryString);
        }

        [Authorize(Policy = "OrganizationSelected")]
        [HttpGet("{configurationId}/svf")]
        public async Task<string> GetSvfRootFileNameAsync([FromRoute] Guid configurationId)
        {
            return await _configurationService.GetSvfRootFileNameAsync(configurationId);
        }

        [HttpGet("{configurationId}/svf/{*svfName}")]
        public async Task<IActionResult> GetSvfAsync([FromRoute] Guid configurationId, [FromRoute] string svfName)
        {
            return File(await _configurationService.GetSvfAsync(configurationId, svfName), "application/octet-stream");
        }

        [Authorize(Policy = "OrganizationSelected")]
        [HttpGet("{configurationId}/parameters")]
        public async Task<VmComponentParameterDefinitions> GetComponentParameterDefinitionsAsync(Guid configurationId)
        {
            return (await _configurationService.GetConfigurationParametersAsync(configurationId)).MapTo<VmComponentParameterDefinitions>(_mapper);
        }

        [Authorize(Policy = "OrganizationSelected")]
        [HttpGet("{id}")]
        public async Task<VmConfiguration> GetConfigurationAsync([FromRoute] Guid id)
        {
            return (await _configurationService.GetConfigurationAsync(id)).MapTo<VmConfiguration>(_mapper);
        }
    }
}
