using DesignGear.ConfigManager.Core.Services.Interfaces;
using DesignGear.ConfigManager.Core.Storage.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignGear.ConfigManager.Core.Jobs {
    public class ConfigurationPushingJob {
        private readonly IConfigurationService _configurationService;
        

        public ConfigurationPushingJob(IConfigurationService configurationService) {
            _configurationService = configurationService ?? throw new ArgumentNullException(nameof(configurationService));
        }

        public void Do() {

        }
    }
}
