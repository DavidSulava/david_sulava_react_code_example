using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignGear.Contracts.Models.ConfigManager {
    public class VmConfigurationCreate {
        public Guid OrganizationId { get; set; }
        public Guid ProductId { get; set; }
        public Guid ProductVersionId { get; set; }
        public Guid AppBundleId { get; set; }
        public ICollection<string> Emails { get; set; }
        public IFormFile ConfigurationPackage { get; set; }
    }
}
