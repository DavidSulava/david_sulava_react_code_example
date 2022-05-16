﻿using DesignGear.Contracts.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignGear.Contracts.Dto.ConfigManager.Configuration {
    public class ConfigurationUpdateDto {
        public Guid ConfigurationId { get; set; }
        public ConfigurationStatus Status { get; set; }
        public SvfStatus SvfStatus { get; set; }
        public Stream ConfigurationPackage { get; set; }
    }
}
