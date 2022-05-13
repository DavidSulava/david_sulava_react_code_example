using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignGear.Contracts.Dto.ConfigManager {
    public class ParameterValueDto {
        public Guid ParameterDefinitionId { get; set; }
        public string Value { get; set; }
    }
}
