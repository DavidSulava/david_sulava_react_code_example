using DesignGear.Contracts.Dto;

namespace DesignGear.Contractor.Core.Services.Interfaces
{
    public interface IOrganizationService
    {
        ICollection<OrganizationDto> GetOrganizationsByUser(Guid userId);

        Guid CreateOrganization(OrganizationCreateDto organization);
    }
}
