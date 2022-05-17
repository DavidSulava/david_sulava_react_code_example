using DesignGear.ConfigManager.Core.Storage.Interfaces;
using DesignGear.Contracts.Dto.ConfigManager;
using DesignGear.ModelPackage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignGear.ConfigManager.Core.Storage {
    public class ConfigurationFileStorage : IConfigurationFileStorage {
        private readonly string _fileBucket = @"C:\DesignGearFiles\Versions\";

        public ConfigurationFileStorage() {

        }

        public async Task<DesignGearModelPackage> SaveConfigurationPackageAsync(ConfigurationPackageDto package) {
            throw new NotImplementedException();
        }

        public async Task SaveSvfAsync(Guid productVersionId, Guid configurationId, Stream svf) {
            throw new NotImplementedException();
        }

        public async Task SaveSvfListAsync(ICollection<Stream> svfList) {
            throw new NotImplementedException();
        }

        public async Task<DesignGearModelPackage> GetPackageModel(Guid productVersionId, Guid configurationId) {
            throw new NotImplementedException();
        }

        public async Task<string> GetSvfNameListAsync(Guid productVersionId, Guid configurationId) {
            throw new NotImplementedException();
        }

        public async Task<Stream> GetSvfAsync(string svfName) {
            throw new NotImplementedException();
        }
    }
}
