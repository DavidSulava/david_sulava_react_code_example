using DesignGear.Common.Enums;
using DesignGear.ConfigManager.Core.Data.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DesignGear.ConfigManager.Core.Data.Entity
{
    public class ParameterDefinition : IGenerateUid
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        
        public int DisplayPriority { get; set; }
        
        [StringLength(300)]
        public string Name { get; set; }
        
        [StringLength(300)]
        public string DisplayName { get; set; }
        
        public ParameterValueType ValueType { get; set; }
        
        [StringLength(300)]
        public string Units { get; set; }

        [ForeignKey("ComponentDefinition")]
        public Guid ComponentDefinitionId { get; set; }
        public virtual ComponentDefinition ComponentDefinition { get; set; }

        public bool IsReadOnly { get; set; }
        
        public bool IsHidden { get; set; }
        
        public bool AllowCustomValues { get; set; }
        
        /*[StringLength(300)]
        public string Value { get; set; }*/
        
        public virtual ICollection<ValueOption> ValueOptions { get; set; }

        public virtual ICollection<ParameterValue> ParameterValues { get; set; }

        [NotMapped]
        public int ParameterId { get; set; }
    }
}
