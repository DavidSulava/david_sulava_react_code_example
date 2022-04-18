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
        public IActionResult CreateOrganization()
        {
            return Ok();
        }

        [HttpGet("organizationbyuser")]
        public ICollection<OrganizationDto> OrganizationsByUser()
        {
            return _organizationService.GetOrganizationsByUser();
        }
    }
}
