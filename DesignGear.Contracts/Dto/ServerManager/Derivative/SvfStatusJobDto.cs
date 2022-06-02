namespace DesignGear.Contracts.Dto.ServerManager.Derivative
{
    public class SvfStatusJobDto
    {
        public string Status { get; set; }
        public IEnumerable<FileStreamDto> SvfFiles { get; set; }
    }
}
