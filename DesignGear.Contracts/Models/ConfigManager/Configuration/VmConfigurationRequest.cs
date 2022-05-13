using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignGear.Contracts.Models.ConfigManager {
    public class VmConfigurationRequest {
        public Guid TemplateConfigurationId { get; set; }
        public string Name { get; set; }
        public ICollection<VmParameterValue> ParameterValues { get; set; }
    }
}
