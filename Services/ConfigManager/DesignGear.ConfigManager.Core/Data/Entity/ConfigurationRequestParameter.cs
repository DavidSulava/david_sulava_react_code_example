using DesignGear.ConfigManager.Core.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignGear.ConfigManager.Core.Data.Entity {
    public class ConfigurationRequestParameter : IGenerateUid, ICreated {
        public Guid Id { get; set; }
        [ForeignKey("ConfigurationRequest")]
        public Guid ConfigurationId { get; set; }
        public Configuration ConfigurationRequest { get; set; }
        [StringLength(1000)]
        public string Value { get; set; }
        public Guid ParameterDefinitionId { get; set; }
        public virtual ParameterDefinition ParameterDefinition { get; set; }
        public DateTime Created { get; set; }
    }
}
