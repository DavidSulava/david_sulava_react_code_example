using DesignGear.Contracts.Dto;

namespace DesignGear.Contractor.Core.Services.Interfaces
{
    public interface IOrganizationService
    {
        Task<ICollection<OrganizationDto>> GetOrganizationsByUser(Guid userId);

        Task<Guid> CreateOrganization(OrganizationCreateDto organization);
    }
}
