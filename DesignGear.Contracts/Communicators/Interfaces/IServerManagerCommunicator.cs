using DesignGear.Contracts.Dto;
using DesignGear.Contracts.Dto.ServerManager.Derivative;
using DesignGear.Contracts.Enums;

namespace DesignGear.Contracts.Communicators.Interfaces
{
    public interface IServerManagerCommunicator
    {
        Task<string> GetSvfAsync(FileStreamDto packageFile, string rootFileName);

        Task<SvfStatus> CheckStatusJobAsync(string urn);

        Task<byte[]> DownloadSvfAsync(string urn);
    }
}
