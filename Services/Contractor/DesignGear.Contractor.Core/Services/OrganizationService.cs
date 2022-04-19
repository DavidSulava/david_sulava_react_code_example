using DesignGear.Contractor.Core.Data;
using DesignGear.Contracts.Dto;
using DesignGear.Contractor.Core.Services.Interfaces;
using DesignGear.Contractor.Core.Data.Entity;
using DesignGear.Common.Enums;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

namespace DesignGear.Contractor.Core.Services
{
    public class OrganizationService : IOrganizationService
    {
        private readonly IMapper _mapper;
        private readonly DataAccessor _dataAccessor;

        public OrganizationService(IMapper mapper, DataAccessor dataAccessor)
        {
            _mapper = mapper;
            _dataAccessor = dataAccessor;
        }

        public Guid CreateOrganization(OrganizationCreateDto organization)
        {
            //TODO: add transaction
            /*var newOrg = new Organization
            {
                Id = Guid.NewGuid(),
                Name = organization.Name,
                Created = DateTime.Now,
                Description = organization.Description,
                TariffId = organization.TariffId
            };*/
            var newOrg = _mapper.Map<Organization>(organization);
            newOrg.Id = Guid.NewGuid();
            /*var newUserAssignment = new UserAssignment
            {
                Id = Guid.NewGuid(),
                UserId = organization.UserId,
                OrganizationId = newOrg.Id,
                Role = UserRole.User
            };*/
            var newUserAssignment = _mapper.Map<UserAssignment>(organization);
            newUserAssignment.OrganizationId = newOrg.Id;
            newUserAssignment.Role = UserRole.User;
            
            _dataAccessor.Editor.Create(newOrg);
            _dataAccessor.Editor.Create(newUserAssignment);
            _dataAccessor.Editor.Save();
            return newOrg.Id;
        }

        public ICollection<OrganizationDto> GetOrganizationsByUser(Guid userId)
        {
            var organizationIds = _dataAccessor.Reader.UserAssignments.
                Where(x => x.UserId == userId).
                Select(x => x.OrganizationId);

            var orgList = _dataAccessor.Reader.Organizations.
                Where(x => organizationIds.Contains(x.Id)).ToList();

            var result = _mapper.Map<List<OrganizationDto>>(orgList);
            return result;
        }
    }
}
