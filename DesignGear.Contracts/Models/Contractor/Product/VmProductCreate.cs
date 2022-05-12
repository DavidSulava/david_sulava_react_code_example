namespace DesignGear.Contracts.Models.Contractor
{
    public class VmProductCreate
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public Guid OrganizationId { get; set; }
    }
}
