using DesignGear.Contractor.Core.Data;
using DesignGear.Contracts.Dto;
using DesignGear.Contractor.Core.Services.Interfaces;

namespace DesignGear.Contractor.Core.Services
{
    public class OrganizationService : IOrganizationService
    {
        private readonly DataAccessor _dataAccessor;

        public OrganizationService(DataAccessor dataAccessor)
        {
            _dataAccessor = dataAccessor;
        }
        public void CreateOrganization(OrganizationDto organization)
        {
            throw new NotImplementedException();
        }

        public ICollection<OrganizationDto> GetOrganizationsByUser()
        {
            return _dataAccessor.Reader.Organizations.Select(x => new OrganizationDto
            {
                Name = x.Name,
                Description = x.Description ?? String.Empty,
                Orgtype = x.OrgType
            }).ToList();
        }
    }
}
