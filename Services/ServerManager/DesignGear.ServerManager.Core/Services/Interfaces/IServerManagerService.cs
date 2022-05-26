namespace DesignGear.ServerManager.Core.Services.Interfaces
{
    public interface IServerManagerService
	{
		Task<string> GetSvfAsync(string filePath, string rootFileName);

		Task<string> ProcessModelAsync(string filePath);
	}
}
