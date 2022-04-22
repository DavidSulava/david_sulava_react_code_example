using Microsoft.AspNetCore.Http;

namespace DesignGear.Contracts.Models
{
    public class VmProductVersionUpdate
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
        public IFormFile ModelFile { get; set; }
        public List<IFormFile> ImageFiles { get; set; }
    }
}
