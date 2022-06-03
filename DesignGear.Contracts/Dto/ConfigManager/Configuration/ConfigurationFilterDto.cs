using DesignGear.Contracts.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignGear.Contracts.Dto.ConfigManager {
    public class ConfigurationFilterDto {
        public ConfigurationStatus? Status;
        public SvfStatus? SvfStatus;
    }
}
