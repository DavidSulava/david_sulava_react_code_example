using DesignGear.Contracts.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignGear.Contracts.Dto.ConfigManager {
    public class ConfigurationCreateDto {
        public Guid OrganizationId { get; set; }
        public Guid ProductId { get; set; }
        public Guid ProductVersionId { get; set; }
        public Guid AppBundleId { get; set; }
        public Stream ConfigurationPackage { get;set; }
    }
}
