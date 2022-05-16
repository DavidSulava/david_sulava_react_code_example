using DesignGear.ConfigManager.Core.Data.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DesignGear.ConfigManager.Core.Data.Entity
{
    public class ValueOption : IGenerateUid, ICreated
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        
        public string Value { get; set; }
        
        [ForeignKey("ParameterDefinition")]
        public Guid ParameterDefinitionId { get; set; }
        public virtual ParameterDefinition ParameterDefinition { get; set; }

        public DateTime Created { get; set; }
    }
}
