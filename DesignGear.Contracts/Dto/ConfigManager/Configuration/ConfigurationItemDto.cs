﻿using DesignGear.Contracts.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignGear.Contracts.Dto.ConfigManager {
    public class ConfigurationItemDto {
        public Guid Id { get; set; }
        public Guid? ParentId { get; set; }
        public string ConfigurationName { get; set; }
        public string ComponentName { get; set; }
        public ConfigurationStatus Status { get; set; }
        public SvfStatus SvfStatus { get; set; }
        public DateTime Created { get; set; }

        public ICollection<ConfigurationItemDto> Childs { get; set; }
    }
}