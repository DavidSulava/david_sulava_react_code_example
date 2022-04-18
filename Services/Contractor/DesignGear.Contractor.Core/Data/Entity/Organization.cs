using DesignGear.Common.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DesignGear.Contractor.Core.Data.Entity
{
    public class Organization
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid OrganizationId { get; set; }

        [StringLength(300)]
        public string Name { get; set; }

        public DateTime Created { get; set; }

        public string? Description { get; set; }

        public double CloudPoints { get; set; }

        public int SpaceUsed { get; set; }

        public OrganizationType OrgType { get; set; }
        
        [ForeignKey("Tariff")]
        public Guid TariffId { get; set; }
        
        public virtual Tariff Tariff { get; set; }
    }
}
