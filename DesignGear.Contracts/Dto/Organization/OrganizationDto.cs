using DesignGear.Common.Enums;

namespace DesignGear.Contracts.Dto
{
    public class OrganizationDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public OrganizationType Orgtype { get; set; }
        public Guid? TariffId { get; set; }
    }
}
