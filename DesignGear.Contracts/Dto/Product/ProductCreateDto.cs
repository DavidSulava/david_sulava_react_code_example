namespace DesignGear.Contracts.Dto
{
    public class ProductCreateDto
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public Guid OrganizationId { get; set; }
    }
}
