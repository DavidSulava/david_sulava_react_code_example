using DesignGear.ConfigManager.Core.Data.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DesignGear.ConfigManager.Core.Data.Entity
{
    public class ConfigurationInstance : IGenerateUid
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public Guid ConfigurationId { get; set; }
        public virtual Configuration Configuration { get; set; }

        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }

        public double XX { get; set; }
        public double YY { get; set; }
        public double ZZ { get; set; }

        public double XY { get; set; }
        public double YX { get; set; }
        public double XZ { get; set; }

        public double ZX { get; set; }
        public double YZ { get; set; }
        public double ZY { get; set; }
    }
}
