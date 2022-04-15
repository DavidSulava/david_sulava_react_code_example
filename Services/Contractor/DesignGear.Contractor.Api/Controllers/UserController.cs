using Microsoft.AspNetCore.Mvc;

namespace DesignGear.Contractor.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        [HttpPost("createuser")]
        public IActionResult CreateUser()
        {
            return Ok();
        }

        [HttpPost("createorganization")]
        public IActionResult CreateOrganization()
        {
            return Ok();
        }

        [HttpGet("organizationbyuser")]
        public IActionResult OrganizationsByUser()
        {
            return Ok();
        }

        [HttpGet("tarifflist")]
        public IActionResult TariffList()
        {
            return Ok();
        }
    }
}
