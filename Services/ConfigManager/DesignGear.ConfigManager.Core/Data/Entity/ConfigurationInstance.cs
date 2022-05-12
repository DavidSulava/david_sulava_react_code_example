using DesignGear.ConfigManager.Core.Data.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DesignGear.ConfigManager.Core.Data.Entity
{
    public class ConfigurationInstance : IGenerateUid
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [ForeignKey(nameof(Configuration))]
        public Guid ConfigurationId { get; set; }
        public virtual Configuration Configuration { get; set; }

        [ForeignKey(nameof(ParentConfiguration))]
        public Guid? ParentConfigurationId { get; set; }
        public virtual Configuration? ParentConfiguration { get; set; }

        public int X { get; set; }
        public int Y { get; set; }
        public int Z { get; set; }

        public int XX { get; set; }
        public int YY { get; set; }
        public int ZZ { get; set; }

        public int XY { get; set; }
        public int YX { get; set; }
        public int XZ { get; set; }

        public int ZX { get; set; }
        public int YZ { get; set; }
        public int ZY { get; set; }
    }
}
