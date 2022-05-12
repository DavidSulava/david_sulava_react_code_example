using DesignGear.Common.Enums;

namespace DesignGear.Contracts.Models.Contractor
{
    public class VmOrganization
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public OrganizationType OrgType { get; set; }
        public Guid TariffId { get; set; }
    }
}

