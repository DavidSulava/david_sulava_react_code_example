using Microsoft.AspNetCore.Http;

namespace DesignGear.ServerManager.Core.Services.Interfaces
{
    public interface IServerManagerService
	{
		Task<string> TranslateSvfAsync(IFormFile packageFile, string rootFileName);

		Task<string> CheckStatusJobAsync(string urn);

		Task<string> ProcessModelAsync(IFormFile packageFile);
	}
}
