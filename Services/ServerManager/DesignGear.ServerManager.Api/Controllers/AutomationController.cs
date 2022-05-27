using DesignGear.ServerManager.Core.Services;
using DesignGear.ServerManager.Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DesignGear.ServerManager.Api.Controllers
{
    [Route("[controller]")]
	[ApiController]
	public class AutomationController : Controller
	{
		private readonly ILogger<AutomationController> _logger;
		private readonly IServerManagerService _serverManagerService;

		public AutomationController(ILogger<AutomationController> logger, IServerManagerService serverManagerService)
		{
			_logger = logger;
			_serverManagerService = serverManagerService;
		}

		[HttpPost]
		public async Task<IActionResult> Get(IFormFile packageFile)
		{
			var url = await _serverManagerService.ProcessModelAsync(packageFile);
			//var url = await _serverManagerService.ProcessModelAsync(@"D:\blocks_and_tables_-_imperial.dwg");
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
