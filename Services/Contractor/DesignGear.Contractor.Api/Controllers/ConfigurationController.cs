using DesignGear.Contracts.Dto;
using DesignGear.Contractor.Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using DesignGear.Contractor.Core.Helpers;
using DesignGear.Contracts.Models.Contractor;
using AutoMapper;
using DesignGear.Common.Extensions;
using DesignGear.Contracts.Models.ConfigManager;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using Newtonsoft.Json;

namespace DesignGear.Contractor.Api.Controllers
{
    [ApiController]
    //[Authorize]
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
        public async Task CreateConfigurationRequestAsync([FromBody] VmConfigurationRequest request)
        {
            await _configurationService.CreateConfigurationRequestAsync(request);
        }

        //[HttpPut]
        //public async Task UpdateConfigurationAsync([FromForm] VmConfigurationUpdate update)
        //{
        //    await _configurationService.UpdateConfigurationAsync(update.MapTo<ConfigurationUpdateDto>(_mapper));
        //}

        //[HttpDelete]
        //public async Task RemoveConfigurationAsync(Guid id)
        //{
        //    await _configurationService.RemoveConfigurationAsync(id);
        //}

        //[HttpGet]
        //public async Task<ICollection<Contracts.Models.Contractor.VmConfigurationItem>> GetConfigurationItemsAsync(Guid productVersionId)
        //{
        //    return (await _configurationService.GetConfigurationItemsAsync(productVersionId)).MapTo<ICollection<Contracts.Models.Contractor.VmConfigurationItem>>(_mapper);
        //}

        [HttpGet]
        public async Task<DataSourceResult> GetConfigurationItemsAsync(Guid productVersionId, [DataSourceRequest] DataSourceRequest dataSourceRequest)
        {
            var queryString = ControllerContext.HttpContext.Request.QueryString.Value;
            return await _configurationService.GetConfigurationItemsAsync(queryString);
        }

        //[HttpGet("{id}")]
        //public async Task<Contracts.Models.Contractor.VmConfiguration> GetConfigurationAsync([FromRoute] Guid id)
        //{
        //    return (await _configurationService.GetConfigurationAsync(id)).MapTo< Contracts.Models.Contractor.VmConfiguration>(_mapper);
        //}

        //[HttpGet]
        //[Route("{id}/Model")]
        //public async Task<ActionResult> GetModelFileAsync([FromRoute] Guid id)
        //{
        //    var modelFile = await _configurationService.GetModelFileAsync(id);
        //    if (modelFile == null || modelFile.Content == null)
        //    {
        //        return Ok();
        //    }
        //    return File(modelFile.Content, modelFile.ContentType, modelFile.FileName);
        //}

        [HttpGet("{configurationId}/svf")]
        public async Task<string> GetSvfRootFileNameAsync([FromRoute] Guid configurationId)
        {
            return await _configurationService.GetSvfRootFileNameAsync(configurationId);
        }

        [HttpGet("{configurationId}/svf/{svfName}")]
        public async Task<IActionResult> GetSvfAsync([FromRoute] Guid configurationId, [FromRoute] string svfName)
        {
            return Ok((await _configurationService.GetSvfAsync(configurationId, svfName)).Content);
        }

        [HttpGet("{configurationId}/parameters")]
        public async Task<VmComponentParameterDefinitions> GetComponentParameterDefinitionsAsync(Guid configurationId)
        {
            return (await _configurationService.GetConfigurationParametersAsync(configurationId)).MapTo<VmComponentParameterDefinitions>(_mapper);
        }
    }
}
