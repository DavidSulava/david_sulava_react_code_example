﻿using DesignGear.Common.Enums;

namespace DesignGear.Contracts.Dto.ConfigManager {
    public class ParameterDefinitionDto
    {
        public Guid Id { get; set; }
        
        public int DisplayPriority { get; set; }
        
        public string Name { get; set; }
        
        public string DisplayName { get; set; }
        
        public ParameterValueType ValueType { get; set; }
        
        public string Units { get; set; }

        public Guid ConfigurationId { get; set; }

        public bool IsReadOnly { get; set; }
        
        public bool IsHidden { get; set; }
        
        public bool AllowCustomValues { get; set; }
        
        public string Value { get; set; }

        public virtual ICollection<ValueOptionDto> ValueOptions { get; set; }
    }
}