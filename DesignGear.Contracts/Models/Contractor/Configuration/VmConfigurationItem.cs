using DesignGear.Common.Enums;

namespace DesignGear.Contracts.Models
{
    public class VmConfigurationItem
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Comment { get; set; }

        public ModelState ModelState { get; set; }

        public Guid ProductVersionId { get; set; }

        public string ModelFile { get; set; }

        public Guid TargetFileId { get; set; }
    }
}
