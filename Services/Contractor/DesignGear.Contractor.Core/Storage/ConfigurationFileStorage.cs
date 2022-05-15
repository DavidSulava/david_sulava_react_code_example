using DesignGear.ModelPackage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignGear.Contractor.Core.Storage {
    public class ConfigurationFileStorage {
        private readonly string _fileBucket = @"C:\DesignGearFiles\Versions\";

        public ConfigurationFileStorage() {

        }

        public async Task<DesignGearModelPackage> SaveConfigurationPackageAsync(Guid productVersionId, Guid configurationId, Stream package) {

        }

        public async Task SaveSvfAsync(Guid productVersionId, Guid configurationId, Stream svf) {

        }

        public async Task SaveSvfListAsync(ICollection<Stream> svfList) {

        }

        public async Task<DesignGearModelPackage> GetPackageModel(Guid productVersionId, Guid configurationId) {

        }

        public async Task<string> GetSvfNameListAsync(Guid productVersionId, Guid configurationId) {

        }

        public async Task<Stream> GetSvfAsync(string svfName) {

        }
    }
}
