namespace DesignGear.Contracts.Models.ConfigManager
{
    //Список параметров конфигурации, в том числе параметры дочерних конфигураций (дерево)
    public class VmComponentParameterDefinitions
    {
        public Guid ComponentId { get; set; }
        public string ComponentName { get; set; }
        public ICollection<VmParameterDefinition> ParameterDefinitions { get; set; }
        public ICollection<VmComponentParameterDefinitions> ChildComponents { get; set; }
    }
}
