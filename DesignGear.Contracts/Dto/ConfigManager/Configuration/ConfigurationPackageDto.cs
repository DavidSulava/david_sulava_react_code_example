using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignGear.Contracts.Dto.ConfigManager {
    public class ConfigurationPackageDto {
        public Guid ProductVersionId { get; set; }
        public Guid ConfigurationId { get; set; }
        public FileStreamDto ConfigurationPackage { get; set; }
    }
}
