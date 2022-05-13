using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignGear.Contracts.Dto.ConfigManager {
    public class ConfigurationRequestDto {
        public Guid TemplateConfigurationId { get; set; }
        public string Name { get; set; }
        public ICollection<ParameterValueDto> ParameterValues { get; set; }
    }
}
