using DesignGear.Common.Enums;
using DesignGear.ConfigManager.Core.Data.Interfaces;
using DesignGear.Contracts.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DesignGear.ConfigManager.Core.Data.Entity
{
    public class Configuration : IGenerateUid
    {
        [Key]
        public Guid Id { get; set; }
        public ConfigurationStatus Status { get; set; }
        public SvfStatus SvfStatus { get; set; }

        [StringLength(300)]
        public string Name { get; set; }

        public string Comment { get; set; }

        public ModelState ModelState { get; set; }

        public Guid TargetFileId { get; set; }
        public Guid RootConfiguration { get; set; }

        [ForeignKey("TemplateConfiguration")]
        public Guid? TemplateConfigurationId { get; set; }
        public virtual Configuration TemplateConfiguration { get; set; }

        [ForeignKey("ComponentDefinition")]
        public Guid ComponentDefinitionId { get; set; }
        public virtual ComponentDefinition ComponentDefinition { get; set; }

        public virtual ICollection<ParameterValue> ParameterValues { get; set; }

        [InverseProperty(nameof(ConfigurationInstance.Configuration))]
        public virtual ICollection<ConfigurationInstance> ConfigurationInstances { get; set; }

        [InverseProperty(nameof(ConfigurationInstance.ParentConfiguration))]
        public virtual ICollection<ConfigurationInstance> ParentConfigurationInstances { get; set; }
    }
}
