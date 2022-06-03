using DesignGear.Contracts.Enums;

namespace DesignGear.Contracts.Dto.ConfigManager
{
    public class ConfigurationUpdateSvfDto {
        public Guid ConfigurationId { get; set; }
        public SvfStatus SvfStatus { get; set; }
        public string URN { get; set; }
    }
}
