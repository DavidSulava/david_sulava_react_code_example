using DesignGear.Common.Enums;

namespace DesignGear.Contracts.Dto
{
    public class UserCreateDto
    {
        public string Email { get; set; }

        public string Password { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Phone { get; set; }

        public UserRole Role { get; set; }
    }
}
