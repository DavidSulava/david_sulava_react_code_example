using Autodesk.Forge.DesignAutomation.Model;
using DesignGear.Contracts.Dto.ServerManager.Derivative;
using DesignGear.Contracts.Enums;
using DesignGear.Contracts.Models.ServerManager.Derivative;
using Microsoft.AspNetCore.Http;

namespace DesignGear.ServerManager.Core.Services.Interfaces
{
    public interface IServerManagerService
	{
		Task<string> TranslateSvfAsync(IFormFile packageFile, string rootFileName);

		Task<SvfStatus> CheckStatusJobAsync(string urn);

		Task<byte[]> DownloadSvfAsync(string urn);

		Task<VmWorkItem> ProcessModelAsync(IFormFile appBundleFile, IFormFile packageFile);

		Task<Status> CheckStatusAsync(string id);
	}
}
