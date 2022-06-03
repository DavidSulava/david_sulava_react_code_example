namespace DesignGear.Contracts.Dto
{
    public class FileStreamDto
    {
        public string FileName { get; set; }

        public string ContentType { get; set; }

        public Stream Content { get; set; }

        public long Length { get; set; }
    }
}
