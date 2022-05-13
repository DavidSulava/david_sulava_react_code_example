namespace DesignGear.Contracts.Models.ConfigManager
{
    //Класс для отображения списка конфигураций в гриде (дерево, т.к. могут быть подкомпоненты)
    public class VmConfigurationItem
    {
        public Guid Id { get; set; }


        public ICollection<VmConfigurationItem> Childs { get; set; }
        //Основной набор свойств
    }
}
