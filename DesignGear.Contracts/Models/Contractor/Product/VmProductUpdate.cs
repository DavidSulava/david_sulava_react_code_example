namespace DesignGear.Contracts.Models
{
    public class VmProductUpdate
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public Guid OrganizationId { get; set; }
    }
}
