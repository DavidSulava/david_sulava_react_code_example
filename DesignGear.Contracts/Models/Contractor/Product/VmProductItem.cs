namespace DesignGear.Contracts.Models.Contractor
{
    public class VmProductItem
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public Guid OrganizationId { get; set; }

        public string CurrentVersion { get; set; }
    }
}
