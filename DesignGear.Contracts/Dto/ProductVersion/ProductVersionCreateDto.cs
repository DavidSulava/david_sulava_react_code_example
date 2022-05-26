﻿using Microsoft.AspNetCore.Http;

namespace DesignGear.Contracts.Dto
{
    public class ProductVersionCreateDto
    {
        public int SequenceNumber { get; set; }
        public string Name { get; set; }
        public string Version { get; set; }
        public string DesignGearVersion { get; set; }
        public string InventorVersion { get; set; }
        public Guid ProductId { get; set; }
        public Guid AppBundleId { get; set; }
        public IFormFile ModelFile { get; set; }
        public ICollection<IFormFile> ImageFiles { get; set; }
        public bool IsCurrent { get; set; }
    }
}
