using DesignGear.Contracts.Dto.ServerManager.Derivative;
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
		public async Task<IActionResult> TranslateSvfAsync([FromForm] IFormFile packageFile, [FromForm] string rootFileName)
		{
			var urn = await _serverManagerService.TranslateSvfAsync(packageFile, rootFileName);
			return new ObjectResult(urn);
		}

		[HttpGet("{urn}")]
		public async Task<IActionResult> CheckStatusJobAsync([FromRoute] string urn)
		{
			var result = await _serverManagerService.CheckStatusJobAsync(urn);
			if (result.Status == "success")
				return File(result.SvfFiles, "application/octet-stream");
			else
				return Ok(result.Status);
		}
	}
}
