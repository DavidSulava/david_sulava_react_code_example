using DesignGear.Contractor.Core.Data.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DesignGear.Contractor.Core.Data.Entity
{
    public class ValueOption : IGenerateUid
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        
        public string Value { get; set; }
        
        [ForeignKey("ParameterDefinition")]
        public Guid ParameterDefinitionId { get; set; }
        public virtual ParameterDefinition ParameterDefinition { get; set; }
    }
}
