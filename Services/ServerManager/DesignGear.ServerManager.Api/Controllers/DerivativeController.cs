using DesignGear.ServerManager.Core;
using DesignGear.ServerManager.Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DesignGear.ServerManager.Api.Controllers
{
    [Route("[controller]")]
	[ApiController]
	public class DerivativeController : ControllerBase
	{
		private readonly ILogger<DerivativeController> _logger;
		private readonly IServerManagerService _serverManagerService;

		public DerivativeController(ILogger<DerivativeController> logger, IServerManagerService serverManagerService)
		{
			_logger = logger;
			_serverManagerService = serverManagerService;
		}

		[HttpPost]
		public async Task<IActionResult> Get(IFormFile packageFile, string rootFileName)
		{
			//var urn = await _serverManagerService.GetSvfAsync(@"D:\Suspension.zip", "Suspension.iam");
			var urn = await _serverManagerService.GetSvfAsync(packageFile, rootFileName);
			return new ObjectResult(urn);
		}
	}
}
