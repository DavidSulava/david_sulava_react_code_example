using DesignGear.Common.Enums;
using DesignGear.Contracts.Enums;

namespace DesignGear.Contracts.Dto.ConfigManager
{
    public class ConfigurationDto
    {
        public Guid Id { get; set; }
        public Guid? ParentId { get; set; }

        public string Name { get; set; }

        public string Comment { get; set; }

        public ModelState ModelState { get; set; }

        public Guid ProductVersionId { get; set; }

        //public string ModelFile { get; set; }

        public Guid TargetFileId { get; set; }
        public ConfigurationStatus Status { get; set; }
        public SvfStatus SvfStatus { get; set; }
        public DateTime Created { get; set; }

        public ICollection<ParameterDefinitionDto> ParameterDefinitions { get; set; }
    }
}
