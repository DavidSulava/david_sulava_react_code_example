using Microsoft.AspNetCore.Mvc;

namespace DesignGear.Contractor.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrganizationController : ControllerBase
    {
        [HttpPost]
        public IActionResult CreateOrganization()
        {
            return Ok();
        }

        [HttpGet("organizationbyuser")]
        public IActionResult OrganizationsByUser()
        {
            return Ok();
        }
    }
}
