namespace DesignGear.ConfigManager.Core.Services.Interfaces
{
    public interface IServerManagerService
	{
		Task<string> GetSvfAsync(string filePath);

		Task<string> ProcessModelAsync(string filePath);
	}
}
