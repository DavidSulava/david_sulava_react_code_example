namespace DesignGear.Contracts.Models
{
    public class VmProductVersion
    {
        public Guid Id { get; set; }
        
        public int SequenceNumber { get; set; }
        
        public string Name { get; set; }
        
        public string Version { get; set; }
        
        public string DesignGearVersion { get; set; }
        
        public string InventorVersion { get; set; }
        
        public DateTime Created { get; set; }
        
        public Guid ProductId { get; set; }
        
        public Guid AppBundleId { get; set; }
        
        public ICollection<string> ImageFiles { get; set; }

        public ICollection<VmConfiguration> Configurations { get; set; }
    }
}
