using DesignGear.Common.Enums;

namespace DesignGear.Contracts.Dto
{
    public class ConfigurationDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Comment { get; set; }

        public ModelState ModelState { get; set; }

        public Guid ProductVersionId { get; set; }

        public string ModelFile { get; set; }

        public Guid TargetFileId { get; set; }

        public ICollection<ParameterDefinitionDto> ParameterDefinitions { get; set; }
    }
}
