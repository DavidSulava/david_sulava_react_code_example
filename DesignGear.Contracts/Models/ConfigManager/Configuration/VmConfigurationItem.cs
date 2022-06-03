using DesignGear.Contracts.Enums;

namespace DesignGear.Contracts.Models.ConfigManager
{
    //Класс для отображения списка конфигураций в гриде (дерево, т.к. могут быть подкомпоненты)
    public class VmConfigurationItem
    {
        public Guid Id { get; set; }
        public Guid? ParentId { get; set; }
        public string ConfigurationName { get; set; }
        public string ComponentName { get; set; }
        public ConfigurationStatus Status { get; set; }
        public SvfStatus SvfStatus { get; set; }
        public DateTime Created { get; set; }

        //public ICollection<VmConfigurationItem> Childs { get; set; }
    }
}
