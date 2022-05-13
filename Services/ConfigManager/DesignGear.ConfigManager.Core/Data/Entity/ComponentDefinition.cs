using DesignGear.ConfigManager.Core.Data.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DesignGear.ConfigManager.Core.Data.Entity
{
    public class ComponentDefinition : IGenerateUid
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [StringLength(300)]
        public string Name { get; set; }

        public Guid TemplateConfigurationId { get; set; }
        public Guid? ParentComponentDefinitionId { get; set; }
        public virtual ComponentDefinition ParentComponentDefinition { get; set; }

        public virtual ICollection<Configuration> Configurations { get; set; }

        public virtual ICollection<ParameterDefinition> ParameterDefinitions { get; set; }
    }
}
