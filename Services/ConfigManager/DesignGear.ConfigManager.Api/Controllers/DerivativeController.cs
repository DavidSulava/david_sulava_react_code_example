using DesignGear.ConfigManager.Core;
using DesignGear.ConfigManager.Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DesignGear.ConfigManager.Api.Controllers
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

		[HttpGet("{id}")]
		public async Task<IActionResult> Get([FromRoute] Guid id)
		{
			var urn = await _serverManagerService.GetSvfAsync(@"D:\Suspension.zip");
			return new ObjectResult(urn);
		}
	}
}
