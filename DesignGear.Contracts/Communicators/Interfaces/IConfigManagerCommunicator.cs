namespace DesignGear.Contracts.Communicators.Interfaces
{
    public interface IConfigManagerCommunicator
    {
        Task<string> ProcessConfigurationAsync(Guid id);

        Task<string> GetSvfAsync(Guid id);
    }
}
