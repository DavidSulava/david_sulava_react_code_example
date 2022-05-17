﻿using DesignGear.Contracts.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignGear.Contracts.Models.ConfigManager {
    public class VmConfigurationRequest {
        public Guid OrganizationId { get; set; }
        public Guid ProductId { get; set; }
        public Guid ProductVersionId { get; set; }
        public Guid AppBundleId { get; set; }
        public ConfigurationStatus Status { get; set; }
        public SvfStatus SvfStatus { get; set; }
        public string Name { get; set; }
        public string Comment { get; set; }
        public ICollection<VmParameterValue> ParameterValues { get; set; }
    }
}
