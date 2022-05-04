namespace DesignGear.Contracts.Models
{
    public class VmAppBundle
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string DesignGearVersion { get; set; }
        public string InventorVersion { get; set; }
    }
}