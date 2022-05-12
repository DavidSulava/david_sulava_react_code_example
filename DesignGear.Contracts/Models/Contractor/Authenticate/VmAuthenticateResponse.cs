namespace DesignGear.Contracts.Models.Contractor
{
    public class VmAuthenticateResponse
    {
        public Guid Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
    }
}
