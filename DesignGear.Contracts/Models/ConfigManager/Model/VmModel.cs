using Microsoft.AspNetCore.Http;

namespace DesignGear.Contracts.Models.ConfigManager
{
    public class VmModel
    {
        public Guid ProductVersionId { get; set; }
        public Guid ProductId { get; set; }
        public Guid OrganizationId { get; set; }

        public IFormFile? ModelFile { get; set; }
    }
}
