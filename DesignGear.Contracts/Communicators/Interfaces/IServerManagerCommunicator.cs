using DesignGear.Contracts.Dto;
using DesignGear.Contracts.Dto.ServerManager.Derivative;
using DesignGear.Contracts.Enums;
using DesignGear.Contracts.Models.ServerManager.Derivative;

namespace DesignGear.Contracts.Communicators.Interfaces
{
    public interface IServerManagerCommunicator
    {
        Task<string> GetSvfAsync(FileStreamDto packageFile, string rootFileName);

        Task<SvfStatus> CheckSvfStatusJobAsync(string urn);

        Task<byte[]> DownloadSvfAsync(string urn);

        Task<VmWorkItem> ProcessModelAsync(byte[] appBundleFile, FileStreamDto packageFile);

        Task<ConfigurationStatus> CheckStatusJobAsync(string workItemId);
    }
}
