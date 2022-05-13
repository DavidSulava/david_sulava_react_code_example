using DesignGear.ConfigManager.Core.Data.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DesignGear.ConfigManager.Core.Data.Entity
{
    public class AppBundle : IGenerateUid
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [StringLength(300)]
        public string Name { get; set; }

        public string Description { get; set; }

        [StringLength(300)]
        public string DesignGearVersion { get; set; }

        [StringLength(300)]
        public string InventorVersion { get; set; }
    }
}
