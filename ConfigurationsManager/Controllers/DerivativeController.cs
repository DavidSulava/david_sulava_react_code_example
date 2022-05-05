using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace ConfigurationsManager.Controllers
{
    [Route("[controller]")]
	[ApiController]
	public class DerivativeController : ControllerBase
	{
		private readonly ILogger<DerivativeController> _logger;

		public DerivativeController(ILogger<DerivativeController> logger)
		{
			_logger = logger;
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> Get([FromRoute] Guid id)
		{
			var urn = await new ServerManager().GetSvfAsync(@"D:\Suspension.zip");
			return new ObjectResult(urn);
		}
	}
}
