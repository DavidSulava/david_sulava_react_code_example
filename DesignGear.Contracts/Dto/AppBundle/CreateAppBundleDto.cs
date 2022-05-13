namespace DesignGear.Contracts.Dto
{
    public class CreateAppBundleDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string DesignGearVersion { get; set; }
        public string InventorVersion { get; set; }
        public AttachmentDto File { get; set; }
    }
}
