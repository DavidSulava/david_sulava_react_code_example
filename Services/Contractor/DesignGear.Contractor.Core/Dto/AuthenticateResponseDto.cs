using DesignGear.Contractor.Core.Data.Entity;

namespace DesignGear.Contractor.Core.Dto
{
    public class AuthenticateResponseDto
    {
        public Guid UserId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }


        public AuthenticateResponseDto(User user, string token)
        {
            UserId = user.UserId;
            FirstName = user.FirstName;
            LastName = user.LastName;
            Email = user.Email;
            Token = token;
        }
    }
}
