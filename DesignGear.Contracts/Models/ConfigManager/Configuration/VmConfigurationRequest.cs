namespace DesignGear.Contracts.Models.ConfigManager
{
    public class VmConfigurationRequest {
        public string Name { get; set; }
        public string Comment { get; set; }
        public Guid BaseConfigurationId { get; set; }
        public ICollection<VmParameterValue> ParameterValues { get; set; }
    }
}
