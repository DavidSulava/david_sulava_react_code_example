using Microsoft.AspNetCore.Mvc;
using DesignGear.Contracts.Models.ConfigManager;
using DesignGear.ConfigManager.Core.Services.Interfaces;
using AutoMapper;
using DesignGear.Contracts.Dto;

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
            //return null;// await _configurationService.CreateConfigurationAsync(create.MapTo<ConfigurationCreateDto>(_mapper));
        }

        [HttpGet]
        public async Task<ICollection<VmConfigurationItem>> GetConfigurationItemsAsync() {
            return null;
        }

        [HttpGet("{id}")]
        public async Task<VmConfiguration> GetConfigurationAsync([FromRoute] Guid id) {
            return null;

        }

        [HttpDelete("{id}")]
        public async Task DeleteConfigurationAsync([FromRoute] Guid id) {

        }

        //Пока возвращаем файл в потоке, потом скорее всего это будет ссылка на файл в хранилище
        [HttpGet("{id}/svf")]
        public async Task<IActionResult> GetSvfAsync([FromRoute] Guid id) {
            return null;

        }

        [HttpGet("parameters")]
        public async Task<VmComponentParameterDefinitions> GetComponentParameterDefinitionsAsync(Guid productVersionId) {
            return null;

        }
    }
}
