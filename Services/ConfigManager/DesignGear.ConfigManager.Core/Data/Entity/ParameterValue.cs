using DesignGear.ConfigManager.Core.Data.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DesignGear.ConfigManager.Core.Data.Entity
{
    public class ParameterValue : IGenerateUid
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [StringLength(300)]
        public string Value { get; set; }

        [ForeignKey("ParameterDefinition")]
        public Guid ParameterDefinitionId { get; set; }
        public virtual ParameterDefinition ParameterDefinition { get; set; }

        [ForeignKey("Configuration")]
        public Guid ConfigurationId { get; set; }
        public virtual Configuration Configuration { get; set; }
    }
}
