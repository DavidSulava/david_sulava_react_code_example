namespace DesignGear.Contracts.Models
{
    public class VmAttachment
    {
        public string FileName { get; set; }

        public string ContentType { get; set; }

        public byte[] Content { get; set; }

        public long Length { get; set; }
    }
}
