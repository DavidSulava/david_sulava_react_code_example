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

        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }

        public float XX { get; set; }
        public float YY { get; set; }
        public float ZZ { get; set; }

        public float XY { get; set; }
        public float YX { get; set; }
        public float XZ { get; set; }

        public float ZX { get; set; }
        public float YZ { get; set; }
        public float ZY { get; set; }
    }
}
