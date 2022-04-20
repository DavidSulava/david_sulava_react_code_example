using DesignGear.Common.Enums;

namespace DesignGear.Contracts.Dto
{
    public class OrganizationDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public OrganizationType Orgtype { get; set; }

    }
}
