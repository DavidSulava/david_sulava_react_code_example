using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
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

		[HttpGet("{id}")]
		public async Task<IActionResult> Get([FromRoute] Guid id)
		{
			var url = await new ServerManager().ProcessModelAsync(@"D:\blocks_and_tables_-_imperial.dwg");
			return new ObjectResult(url);
			/*var filePath = $"{_fileBucket}{id}\\model\\";
			var di = new DirectoryInfo(filePath);
			if (di.Exists)
			{
				var fullName = di.EnumerateFiles().FirstOrDefault()?.FullName;
				if (!string.IsNullOrEmpty(fullName))
				{
					var url = await new ServerManager().ProcessModelAsync(fullName);
					return new ObjectResult(url);
				}
			}

			return new NotFoundObjectResult(filePath);*/
		}
	}
}
