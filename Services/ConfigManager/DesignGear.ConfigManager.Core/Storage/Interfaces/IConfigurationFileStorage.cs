using DesignGear.Contracts.Dto.ConfigManager;
using DesignGear.Contracts.Dto.ConfigManager.Configuration;
using DesignGear.ModelPackage;

namespace DesignGear.ConfigManager.Core.Storage.Interfaces {
    public interface IConfigurationFileStorage {
        Task<DesignGearModelPackage> GetPackageModel(Guid productVersionId, Guid configurationId);
        Task<Stream> GetSvfAsync(string svfName);
        Task<string> GetSvfNameListAsync(Guid productVersionId, Guid configurationId);
        Task<DesignGearModelPackage> SaveConfigurationPackageAsync(ConfigurationPackageDto package);
        Task SaveSvfAsync(Guid productVersionId, Guid configurationId, Stream svf);
        Task SaveSvfListAsync(ICollection<Stream> svfList);
    }
}