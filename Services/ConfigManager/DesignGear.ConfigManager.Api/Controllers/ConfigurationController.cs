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

        /*
         *  Этот метод у нас перееезжает в ModelController
         */
        //[HttpPost]
        //public async Task CreateConfigurationAsync([FromForm] VmConfigurationCreate create) {
        //    await _configurationService.CreateConfigurationAsync(create.MapTo<ConfigurationCreateDto>(_mapper));
        //}

        [HttpPost]
        public async Task CreateConfigurationRequestAsync([FromForm] VmConfigurationRequest request) {
            await _configurationService.CreateConfigurationRequestAsync(request.MapTo<ConfigurationRequestDto>(_mapper));
        }

        [HttpGet]
        public async Task<ICollection<VmConfigurationItem>> GetConfigurationItemsAsync() {
            return null;
        }

        //[HttpGet("{id}")]
        //public async Task<VmConfiguration> GetConfigurationAsync([FromRoute] Guid id) {
        //    return null;
        //}

        //[HttpDelete("{id}")]
        //public async Task DeleteConfigurationAsync([FromRoute] Guid id) {

        //}

        //Пока возвращаем файл в потоке, потом скорее всего это будет ссылка на файл в хранилище
        [HttpGet("{id}/svf")]
        public async Task<IActionResult> GetSvfAsync([FromRoute] Guid id) {
            return null;

        }

        /*
         * Уезжает в ModelController
         */

        //[HttpGet("parameters")]
        //public async Task<VmComponentParameterDefinitions> GetComponentParameterDefinitionsAsync(Guid productVersionId) {
        //    return null;

        //}
    }
}
