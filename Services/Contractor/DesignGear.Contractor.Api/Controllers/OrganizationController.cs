using DesignGear.Contracts.Dto;
using DesignGear.Contractor.Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using DesignGear.Contractor.Core.Helpers;
using DesignGear.Contracts.Models;
using AutoMapper;
using DesignGear.Common.Extensions;

namespace DesignGear.Contractor.Api.Controllers
{
    [ApiController]
    [Authorize]
    [Route("[controller]")]
    public class OrganizationController : ControllerBase
    {
        private readonly IOrganizationService _organizationService;
        private readonly IMapper _mapper;

        public OrganizationController(IOrganizationService organizationService, IMapper mapper)
        {
            _organizationService = organizationService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<Guid> CreateOrganizationAsync(VmOrganizationCreate organization)
        {
            return await _organizationService.CreateOrganizationAsync(organization.MapTo<OrganizationCreateDto>(_mapper));
        }

        [HttpGet("byuser")]
        public async Task<ICollection<VmOrganization>> GetOrganizationsAsync(Guid userId)
        {
            return (await _organizationService.GetOrganizationsByUserAsync(userId)).MapTo<ICollection<VmOrganization>>(_mapper);
        }
    }
}
