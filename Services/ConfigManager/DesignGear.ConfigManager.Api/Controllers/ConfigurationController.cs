using Microsoft.AspNetCore.Mvc;
using DesignGear.Contracts.Models.ConfigManager;

namespace DesignGear.ConfigManager.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ConfigurationController : ControllerBase
    {
        public ConfigurationController()
        {

        }

        [HttpPost]
        public async Task CreateConfigurationAsync([FromRoute] VmConfigurationCreate create) {
            var a = 1;
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
