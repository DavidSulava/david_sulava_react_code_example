namespace DesignGear.Contracts.Models.Contractor
{
    public class VmAttachment
    {
        public string FileName { get; set; }

        public string ContentType { get; set; }

        public byte[] Content { get; set; }

        public long Length { get; set; }
    }
}
