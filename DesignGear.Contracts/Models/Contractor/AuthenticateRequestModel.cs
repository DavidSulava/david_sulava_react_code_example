using System.ComponentModel.DataAnnotations;

namespace DesignGear.Contracts.Models.Contractor
{
    public class AuthenticateRequestModel
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
