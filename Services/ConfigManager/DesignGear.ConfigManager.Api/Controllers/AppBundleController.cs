using Microsoft.AspNetCore.Mvc;

namespace DesignGear.ConfigManager.Api.Controllers {
    public class VmAppBundleCreate {

    }

    public class VmAppBundleItem {

    }

    public class AppBundleController : ControllerBase {
        public AppBundleController() {

        }

        [HttpPost]
        public async Task CreateAppBundleAsync(VmAppBundleCreate create) {

        }

        [HttpGet]
        public async Task<ICollection<VmAppBundleItem>> GetAppBundleListAsync() {
            
        }
    }
}
