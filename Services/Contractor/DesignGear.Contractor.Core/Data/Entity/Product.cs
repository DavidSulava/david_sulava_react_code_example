using DesignGear.Contractor.Core.Data.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DesignGear.Contractor.Core.Data.Entity
{
    public class Product : IGenerateUid
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [StringLength(300)]
        public string Name { get; set; }

        public string Description { get; set; }

        [ForeignKey("Organization")]
        public Guid OrganizationId { get; set; }
        public virtual Organization Organization { get; set; }

        public virtual ICollection<ProductVersion> ProductVersions { get; set; }

        //[ForeignKey("CurrentProductVersion")]
        public Guid? CurrentVersionId { get; set; }
        public virtual ProductVersion CurrentProductVersion { get; set; }
    }
}
