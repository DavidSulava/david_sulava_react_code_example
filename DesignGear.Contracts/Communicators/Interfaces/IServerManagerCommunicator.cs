using DesignGear.Contracts.Dto;
using DesignGear.Contracts.Dto.ServerManager.Derivative;

namespace DesignGear.Contracts.Communicators.Interfaces
{
    public interface IServerManagerCommunicator
    {
        Task<string> GetSvfAsync(FileStreamDto packageFile, string rootFileName);

        Task<SvfStatusJobDto> CheckStatusJobAsync(string urn);
    }
}
