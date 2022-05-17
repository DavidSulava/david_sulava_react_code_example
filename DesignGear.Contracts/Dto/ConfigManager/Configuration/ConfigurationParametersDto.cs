using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignGear.Contracts.Dto.ConfigManager {
    public class ConfigurationParametersDto {
        public Guid ConfigurationId { get; set; }
        public string ConfigurationName { get; set; }
        public string ComponentName { get; set; }
        public ICollection<ParameterDefinitionDto> Parameters { get; set; }
        public ICollection<ConfigurationParametersDto> Childs { get; set; }
    }
}
