using DesignGear.Common.Enums;
using DesignGear.Contracts.Enums;

namespace DesignGear.Contracts.Models.ConfigManager
{
    //Класс, содержащий подробную информацию о конфигурации
    public class VmConfiguration
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Comment { get; set; }

        public ModelState ModelState { get; set; }

        public Guid ProductVersionId { get; set; }

        public Guid TargetFileId { get; set; }

        public ICollection<VmParameterDefinition> ParameterDefinitions { get; set; }

        public Guid? ParentId { get; set; }
        public string ComponentName { get; set; }
        public ConfigurationStatus Status { get; set; }
        public SvfStatus SvfStatus { get; set; }
        public DateTime Created { get; set; }
    }
}
