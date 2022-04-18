using DesignGear.Contracts.Dto;
using DesignGear.Contractor.Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

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

        [HttpPost]
        public Guid CreateOrganization(OrganizationCreateDto organization)
        {
            return _organizationService.CreateOrganization(organization);
        }

        [HttpGet("organizationbyuser")]
        public ICollection<OrganizationDto> OrganizationsByUser()
        {
            return _organizationService.GetOrganizationsByUser();
        }
    }
}
