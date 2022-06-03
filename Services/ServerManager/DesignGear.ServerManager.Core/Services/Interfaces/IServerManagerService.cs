using DesignGear.Contracts.Dto.ServerManager.Derivative;
using Microsoft.AspNetCore.Http;

namespace DesignGear.ServerManager.Core.Services.Interfaces
{
    public interface IServerManagerService
	{
		Task<string> TranslateSvfAsync(IFormFile packageFile, string rootFileName);

		Task<SvfStatusJobDto> CheckStatusJobAsync(string urn);

		Task<string> ProcessModelAsync(IFormFile packageFile);
	}
}
