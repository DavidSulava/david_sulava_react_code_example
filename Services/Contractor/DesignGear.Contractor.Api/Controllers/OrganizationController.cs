using DesignGear.Contracts.Dto;
using DesignGear.Contractor.Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using DesignGear.Contractor.Core.Helpers;

namespace DesignGear.Contractor.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrganizationController : ControllerBase
    {
        private readonly IOrganizationService _organizationService;

        public OrganizationController(IOrganizationService organizationService)
        {
            _organizationService = organizationService;
        }

        [Authorize]
        [HttpPost]
        public Guid CreateOrganization(OrganizationCreateDto organization)
        {
            return _organizationService.CreateOrganization(organization);
        }

        [Authorize]
        [HttpGet("organizationbyuser")]
        public ICollection<OrganizationDto> OrganizationsByUser()
        {
            return _organizationService.GetOrganizationsByUser();
        }
    }
}
