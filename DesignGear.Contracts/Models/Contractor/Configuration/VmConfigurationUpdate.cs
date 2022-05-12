using DesignGear.Common.Enums;

namespace DesignGear.Contracts.Models.Contractor
{
    public class VmConfigurationUpdate
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Comment { get; set; }

        public ModelState ModelState { get; set; }

        public Guid ProductVersionId { get; set; }

        public Guid TargetFileId { get; set; }

        public ICollection<VmParameterDefinition> ParameterDefinitions { get; set; }
    }
}
