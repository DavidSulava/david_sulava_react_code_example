using DesignGear.Common.Enums;

namespace DesignGear.Contracts.Models.Contractor
{
    public class VmParameterDefinition
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
    }
}
