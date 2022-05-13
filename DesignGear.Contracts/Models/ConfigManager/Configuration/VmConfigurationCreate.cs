using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignGear.Contracts.Models.ConfigManager {
    public class VmConfigurationCreate {
        public Guid OrganizationId { get; set; }
        public Guid ProductId { get; set; }
        public Guid ProductVersionId { get; set; }
    }
}
