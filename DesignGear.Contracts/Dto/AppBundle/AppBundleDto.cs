namespace DesignGear.Contracts.Dto
{
    public class AppBundleDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string DesignGearVersion { get; set; }
        public string InventorVersion { get; set; }
        public string FileName { get; set; }
        public byte[] Content { get; set; }
    }
}
