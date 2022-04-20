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

        [Authorize]
        [HttpPost]
        public async Task<Guid> CreateOrganization(VmOrganizationCreate organization)
        {
            return await _organizationService.CreateOrganization(organization.MapTo<OrganizationCreateDto>(_mapper));
        }

        [Authorize]
        [HttpGet("byuser")]
        public async Task<ICollection<VmOrganization>> OrganizationsByUser(Guid userId)
        {
            return (await _organizationService.GetOrganizationsByUser(userId)).MapTo<ICollection<VmOrganization>>(_mapper);
        }
    }
}
