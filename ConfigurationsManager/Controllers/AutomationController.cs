using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace ConfigurationsManager.Controllers
{
    [Route("[controller]")]
	[ApiController]
	public class AutomationController : Controller
	{
		private readonly ILogger<AutomationController> _logger;

		public AutomationController(ILogger<AutomationController> logger)
		{
			_logger = logger;
		}

		[HttpGet]
		public async Task<IActionResult> Get()
		{
			var url = await new ServerManager().ProcessModelAsync(@"D:\blocks_and_tables_ - _imperial.dwg");
			return new ObjectResult(url);
		}
	}
}
