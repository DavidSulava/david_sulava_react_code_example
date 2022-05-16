using DesignGear.Common.Enums;

namespace DesignGear.Contracts.Models.Contractor
{
    public class VmOrganizationCreate
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public OrganizationType OrgType { get; set; }
        public Guid UserId { get; set; }
        public Guid? TariffId { get; set; }
    }
}

