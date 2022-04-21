using DesignGear.Contracts.Dto;

namespace DesignGear.Contractor.Core.Services.Interfaces
{
    public interface IOrganizationService
    {
        Task<ICollection<OrganizationDto>> GetOrganizationsByUserAsync(Guid userId);

        Task<Guid> CreateOrganizationAsync(OrganizationCreateDto organization);
    }
}
