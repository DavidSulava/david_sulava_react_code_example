using DesignGear.Contractor.Core.Data.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DesignGear.Contractor.Core.Data.Entity
{
    public class ProductVersion : IGenerateUid, ICreated
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public int SequenceNumber { get; set; }

        [StringLength(300)]
        public string Name { get; set; }

        [StringLength(300)]
        public string Version { get; set; }

        [StringLength(300)]
        public string DesignGearVersion { get; set; }

        [StringLength(300)]
        public string InventorVersion { get; set; }

        public DateTime Created { get; set; }

        [ForeignKey("Product")]
        public Guid ProductId { get; set; }
        public virtual Product Product { get; set; }

        [ForeignKey("AppBundle")]
        public Guid AppBundleId { get; set; }
        public virtual AppBundle AppBundle { get; set; }

        //public ICollection<ParameterDefinition> ParameterDefinitions { get; set; }

        public virtual ICollection<Configuration> Configurations { get; set; }
    }
}

