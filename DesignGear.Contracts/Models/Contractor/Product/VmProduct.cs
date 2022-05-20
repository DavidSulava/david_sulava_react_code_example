namespace DesignGear.Contracts.Models.Contractor
{
    public class VmProduct
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public Guid OrganizationId { get; set; }

        public string CurrentVersion { get; set; }

        public ICollection<VmProductVersion> ProductVersions { get; set; }
    }
}
