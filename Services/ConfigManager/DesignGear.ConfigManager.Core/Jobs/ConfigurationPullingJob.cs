using DesignGear.ConfigManager.Core.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignGear.ConfigManager.Core.Jobs {
    public class ConfigurationPullingJob {
        private readonly IConfigurationService _configurationService;

        public ConfigurationPullingJob(IConfigurationService configurationService) {
            _configurationService = configurationService ?? throw new ArgumentNullException(nameof(configurationService));
        }

        public void Do() {

        }
    }
}
