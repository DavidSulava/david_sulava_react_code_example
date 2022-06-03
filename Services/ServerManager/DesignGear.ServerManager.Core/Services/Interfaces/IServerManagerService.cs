using DesignGear.Contracts.Dto.ServerManager.Derivative;
using DesignGear.Contracts.Enums;
using Microsoft.AspNetCore.Http;

namespace DesignGear.ServerManager.Core.Services.Interfaces
{
    public interface IServerManagerService
	{
		Task<string> TranslateSvfAsync(IFormFile packageFile, string rootFileName);

		Task<SvfStatus> CheckStatusJobAsync(string urn);

		Task<byte[]> DownloadSvfAsync(string urn);

		Task<string> ProcessModelAsync(IFormFile packageFile);
	}
}
