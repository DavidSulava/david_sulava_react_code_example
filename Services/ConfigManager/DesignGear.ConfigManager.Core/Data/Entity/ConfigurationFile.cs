using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignGear.ConfigManager.Core.Data.Entity {
    public class ConfigurationFile {
        public Guid Id { get; set; }
        public Guid ConfigurationId { get; set; }
        [StringLength(4000)]        
        public string FileName { get; set; }

        public virtual Configuration Configuration { get; set; }
    }
}
