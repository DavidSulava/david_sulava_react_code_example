using Microsoft.AspNetCore.Mvc;

namespace DesignGear.ConfigManager.Api.Controllers {
    public class VmModel {
        public Guid ProductVersionId { get; set; }
        public Guid ProductId { get; set; }
        public Guid OrganizationId { get; set; }

        public IFormFile? ModelFile { get; set; }
    }

    public class ModelController : ControllerBase {
        public ModelController() {

        }

        public async Task SetModelAsync([FromForm] VmModel model) {

        }
    }
}
