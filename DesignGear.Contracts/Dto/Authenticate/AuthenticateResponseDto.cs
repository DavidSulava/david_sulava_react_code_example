namespace DesignGear.Contracts.Dto
{
    public class AuthenticateResponseDto
    {
        public Guid Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
    }
}
