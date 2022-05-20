using DesignGear.Common.Enums;

namespace DesignGear.Contracts.Models.Contractor
{
    public class VmConfigurationCreate
    {
        public Guid TemplateConfigurationId { get; set; }

        public string Name { get; set; }

        public string Comment { get; set; }

        public ModelState ModelState { get; set; }

        public Guid ProductVersionId { get; set; }

        public Guid TargetFileId { get; set; }

        public ICollection<VmParameter> ParameterDefinitions { get; set; }
    }
}
