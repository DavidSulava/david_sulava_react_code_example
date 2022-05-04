﻿namespace DesignGear.Contracts.Dto
{
    public class ProductDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public Guid OrganizationId { get; set; }

        public ICollection<ProductVersionDto> ProductVersions { get; set; }
    }
}