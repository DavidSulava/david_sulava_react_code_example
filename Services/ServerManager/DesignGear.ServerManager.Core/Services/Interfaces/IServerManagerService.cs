using Microsoft.AspNetCore.Http;

namespace DesignGear.ServerManager.Core.Services.Interfaces
{
    public interface IServerManagerService
	{
		Task<string> GetSvfAsync(IFormFile packageFile, string rootFileName);

		Task<string> ProcessModelAsync(IFormFile packageFile);
	}
}
