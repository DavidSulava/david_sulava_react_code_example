using DesignGear.Contracts.Enums;

namespace DesignGear.Contracts.Dto.ConfigManager
{
    public class ConfigurationStatusUpdateDto {
        public Guid ConfigurationId { get; set; }
        public ConfigurationStatus Status { get; set; }
        public SvfStatus SvfStatus { get; set; }
        public FileStreamDto ConfigurationPackage { get; set; }
    }
}
