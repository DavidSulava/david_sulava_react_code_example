using DesignGear.Contracts.Dto;

namespace DesignGear.Contracts.Dto
{
    public class CreateConfigurationRequest
    {
        public Guid OrganizationId { get; set; }
        public Guid ProductId { get; set; }
        public Guid ProductVersionId { get; set; }
    }
}
