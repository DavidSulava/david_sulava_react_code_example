using DesignGear.Contractor.Core.Data;
using DesignGear.Contracts.Dto;
using DesignGear.Contractor.Core.Services.Interfaces;
using DesignGear.Contractor.Core.Data.Entity;
using DesignGear.Common.Enums;

namespace DesignGear.Contractor.Core.Services
{
    public class OrganizationService : IOrganizationService
    {
        private readonly DataAccessor _dataAccessor;

        public OrganizationService(DataAccessor dataAccessor)
        {
            _dataAccessor = dataAccessor;
        }

        public Guid CreateOrganization(OrganizationCreateDto organization)
        {
            var newOrg = new Organization
            {
                OrganizationId = Guid.NewGuid(),
                Name = organization.Name,
                Created = DateTime.Now,
                Description = organization.Description,
                TariffId = organization.TariffId
            };
            var newUserAssignment = new UserAssignment
            {
                UserAssignmentId = Guid.NewGuid(),
                UserId = organization.UserId,
                OrganizationId = newOrg.OrganizationId,
                Role = UserRole.User
            };
            _dataAccessor.Editor.Create(newOrg);
            _dataAccessor.Editor.Create(newUserAssignment);
            _dataAccessor.Editor.Save();
            return newOrg.OrganizationId;
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
