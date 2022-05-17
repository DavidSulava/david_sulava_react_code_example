using DesignGear.ConfigManager.Core.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignGear.ConfigManager.Core.Data.Entity {
    public class ConfigurationRequestEmail : IGenerateUid, ICreated {
        public Guid Id { get; set; }

        [ForeignKey("ConfigurationRequest")]
        public Guid ConfigurationId { get; set; }
        public Configuration ConfigurationRequest { get; set; }

        [StringLength(255)]
        public string Email { get; set; }
        public DateTime Created { get; set; }
    }
}
