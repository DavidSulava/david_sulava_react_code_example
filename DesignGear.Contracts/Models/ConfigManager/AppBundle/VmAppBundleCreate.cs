﻿using Microsoft.AspNetCore.Http;

namespace DesignGear.Contracts.Models.ConfigManager
{
    public class VmAppBundleCreate
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string DesignGearVersion { get; set; }
        public string InventorVersion { get; set; }

        public IFormFile File { get; set; }
    }
}