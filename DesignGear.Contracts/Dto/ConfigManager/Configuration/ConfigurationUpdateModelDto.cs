using DesignGear.Contracts.Enums;

namespace DesignGear.Contracts.Dto.ConfigManager
{
    public class ConfigurationUpdateModelDto {
        public Guid ConfigurationId { get; set; }
        public ConfigurationStatus Status { get; set; }
        public string WorkItemId { get; set; }
        public string WorkItemUrl { get; set; }
    }
}
