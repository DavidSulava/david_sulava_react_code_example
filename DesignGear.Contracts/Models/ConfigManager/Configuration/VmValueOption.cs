namespace DesignGear.Contracts.Models.ConfigManager
{
    public class VmValueOption
    {
        public Guid Id { get; set; }

        public string Value { get; set; }

        public Guid ParameterDefinitionId { get; set; }

        public DateTime Created { get; set; }
    }
}
