using DesignGear.Common.Enums;
using DesignGear.Contractor.Core.Data.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DesignGear.Contractor.Core.Data.Entity
{
    public class User : IGenerateUid, ICreated
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [StringLength(300)]
        public string Email { get; set; }

        [StringLength(100)]
        public string Password { get; set; }

        [StringLength(300)]
        public string? FirstName { get; set; }

        [StringLength(300)]
        public string? LastName { get; set; }

        [StringLength(100)]
        public string? Phone { get; set; }

        public DateTime Created { get; set; }

        public UserRole Role { get; set; }
    }
}
