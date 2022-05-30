using DesignGear.Contracts.Dto;

namespace DesignGear.Contracts.Communicators.Interfaces
{
    public interface IServerManagerCommunicator
    {
        Task<string> GetSvfAsync(FileStreamDto packageFile, string rootFileName);
    }
}
