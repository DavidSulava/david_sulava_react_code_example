namespace DesignGear.Contracts.Models.ConfigManager
{
    //Список значений параметров компонента
    public class VmConfigurationParameterValues
    {
        public Guid ComponentId { get; set; }
        public ICollection<VmParameterValue> ParameterValues { get; set; }
    }
}
