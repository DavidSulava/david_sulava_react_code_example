using Microsoft.AspNetCore.Mvc;

namespace DesignGear.Contractor.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TariffController : ControllerBase
    {
        [HttpGet]
        public IActionResult TariffList()
        {
            return Ok();
        }
    }
}
