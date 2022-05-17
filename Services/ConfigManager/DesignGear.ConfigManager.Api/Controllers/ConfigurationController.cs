using Microsoft.AspNetCore.Mvc;
using DesignGear.Contracts.Models.ConfigManager;
using DesignGear.ConfigManager.Core.Services.Interfaces;
using AutoMapper;
using DesignGear.Contracts.Dto;
using DesignGear.Contracts.Dto.ConfigManager;
using DesignGear.Common.Extensions;

namespace DesignGear.ConfigManager.Api.Controllers {
    [ApiController]
    [Route("[controller]")]
    public class ConfigurationController : ControllerBase {
        private readonly IConfigurationService _configurationService;
        private readonly IMapper _mapper;

        public ConfigurationController(IConfigurationService configurationService, IMapper mapper) {
            _configurationService = configurationService;
            _mapper = mapper;
        }


        [HttpPost]
        public async Task CreateConfigurationAsync([FromForm] VmConfigurationCreate create) {
            await _configurationService.CreateConfigurationFromPackageAsync(create.MapTo<ConfigurationCreateDto>(_mapper));
        }

        [HttpPost("request")]
        public async Task CreateConfigurationRequestAsync([FromBody] VmConfigurationRequest request) {
            await _configurationService.CreateConfigurationRequestAsync(request.MapTo<ConfigurationRequestDto>(_mapper));
        }

        [HttpGet]
        public async Task<ICollection<VmConfigurationItem>> GetConfigurationItemsAsync() {
            return null;
        }

        [HttpGet("{configurationId}/svf")]
        public async Task<IEnumerable<string>> GetSvfListAsync([FromRoute] Guid configurationId) {
            return null;
        }

        [HttpGet("{configurationId}/svf/{svfName}")]
        public async Task<IActionResult> GetSvfAsync([FromRoute] Guid configurationId, [FromRoute] string svfName) {
            return null;
        }

        [HttpGet("parameters")]
        public async Task<VmComponentParameterDefinitions> GetComponentParameterDefinitionsAsync(Guid productVersionId) {
            return null;
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
