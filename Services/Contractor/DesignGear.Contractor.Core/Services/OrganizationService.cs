using DesignGear.Contractor.Core.Data;
using DesignGear.Contracts.Dto;
using DesignGear.Contractor.Core.Services.Interfaces;
using DesignGear.Contractor.Core.Data.Entity;
using DesignGear.Common.Enums;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using AutoMapper.QueryableExtensions;

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

        public async Task<Guid> CreateOrganizationAsync(OrganizationCreateDto create)
        {
            if (create == null)
            {
                throw new ArgumentNullException(nameof(create));
            }

            var newOrg = _mapper.Map<Organization>(create);
            newOrg.Id = Guid.NewGuid();
            var newUserAssignment = _mapper.Map<UserAssignment>(create);
            newUserAssignment.OrganizationId = newOrg.Id;
            newUserAssignment.Role = UserRole.User;
            
            _dataAccessor.Editor.Create(newOrg);
            _dataAccessor.Editor.Create(newUserAssignment);
            await _dataAccessor.Editor.SaveAsync();
            return newOrg.Id;
        }

        public async Task<ICollection<OrganizationDto>> GetOrganizationsByUserAsync(Guid userId)
        {
            var organizationIds = _dataAccessor.Reader.UserAssignments.
                Where(x => x.UserId == userId).
                Select(x => x.OrganizationId);

            return await _dataAccessor.Reader.Organizations.Where(x => organizationIds.Contains(x.Id))
                .ProjectTo<OrganizationDto>(_mapper.ConfigurationProvider).ToListAsync();
        }
    }
}
