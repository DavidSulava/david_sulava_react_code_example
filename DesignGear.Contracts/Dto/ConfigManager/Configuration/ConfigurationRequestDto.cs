namespace DesignGear.Contracts.Dto.ConfigManager
{
    public class ConfigurationRequestDto {
        public string Name { get; set; }
        public string Comment { get; set; }
        public Guid BaseConfigurationId { get; set; }
        public ICollection<ParameterValueDto> ParameterValues { get; set; }
    }
}
