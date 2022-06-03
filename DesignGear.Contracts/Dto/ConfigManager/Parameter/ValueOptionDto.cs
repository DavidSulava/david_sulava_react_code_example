namespace DesignGear.Contracts.Dto.ConfigManager
{
    public class ValueOptionDto
    {
        public Guid Id { get; set; }

        public string Value { get; set; }

        public Guid ParameterDefinitionId { get; set; }

        public DateTime Created { get; set; }
    }
}
