using DesignGear.Common.Enums;
using DesignGear.Contractor.Core.Data.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DesignGear.Contractor.Core.Data.Entity
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
        
        //public string ConfigurationId { get; set; }
        
        public bool IsReadOnly { get; set; }
        
        public bool IsHidden { get; set; }
        
        public bool AllowCustomValues { get; set; }
        
        //public string UniqueId { get; set; }
        
        [StringLength(300)]
        public string Value { get; set; }
        
        public ICollection<ValueOption> ValueOptions { get; set; }
        
        [ForeignKey("ProductVersion")]
        public Guid ProductVersionId { get; set; }
        public virtual ProductVersion ProductVersion { get; set; }

        [NotMapped]
        public int ParameterId { get; set; }
    }
}
