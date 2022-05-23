using Microsoft.AspNetCore.Http;

namespace DesignGear.Contracts.Dto.ConfigManager
{
    public class ConfigurationCreateDto {
        public Guid OrganizationId { get; set; }
        public Guid ProductId { get; set; }
        public Guid ProductVersionId { get; set; }
        public Guid AppBundleId { get; set; }
        public ICollection<string> Emails { get; set; }
        public IFormFile ConfigurationPackage { get;set; }
    }
}
