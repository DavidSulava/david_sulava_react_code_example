using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignGear.Contracts.Models.ConfigManager {
    public class VmParameterValue {
        public Guid ParameterDefinitionId { get; set; }
        public string Value { get; set; }
    }
}
