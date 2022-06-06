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
		public async Task<IActionResult> ProcessModelAsync(IFormFile appBundleFile, IFormFile packageFile)
		{
			var result = await _serverManagerService.ProcessModelAsync(appBundleFile, packageFile);
			return new ObjectResult(result);

			//var url = await _serverManagerService.ProcessModelAsync(@"D:\blocks_and_tables_-_imperial.dwg");
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

		[HttpGet("{id}")]
		public async Task<IActionResult> CheckWorkItemStatusJobAsync([FromRoute] string id)
		{
			return Ok(await _serverManagerService.CheckStatusAsync(id));
		}

		//[HttpGet("{url}")]
		//public async Task<IActionResult> DownloadSvfAsync([FromRoute] string url)
		//{
		//	var result = new byte[0];// await _serverManagerService.DownloadSvfAsync(urn);
		//	if (result != null)
		//		return File(result, "application/octet-stream");
		//	else
		//		return Ok();
		//}
	}
}
