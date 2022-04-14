using System.ComponentModel.DataAnnotations;

namespace DesignGear.Contractor.Core.Dto
{
    public class AuthenticateRequest
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
