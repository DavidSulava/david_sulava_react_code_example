using DesignGear.Common.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DesignGear.Contractor.Core.Data.Entity
{
    public class UserAssignment
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid UserAssignmentId { get; set; }

        [ForeignKey("User")]
        public Guid UserId { get; set; }
        public virtual User User { get; set; }

        [ForeignKey("Organization")]
        public Guid OrganizationId { get; set; }
        public virtual Organization Organization { get; set; }

        public UserRole Role { get; set; }
    }
}
