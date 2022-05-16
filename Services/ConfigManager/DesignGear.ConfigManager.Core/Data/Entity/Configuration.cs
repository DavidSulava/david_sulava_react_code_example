using DesignGear.Common.Enums;
using DesignGear.ConfigManager.Core.Data.Interfaces;
using DesignGear.Contracts.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DesignGear.ConfigManager.Core.Data.Entity
{
    public class Configuration : IGenerateUid, ICreated
    {
        [Key]
        public Guid Id { get; set; }
        [StringLength(200)]
        public string UniqueId { get; set; }
        public ConfigurationStatus Status { get; set; }
        public SvfStatus SvfStatus { get; set; }
        [StringLength(2000)]
        public string ErrorMessage { get; set; }

        [StringLength(300)]
        public string Name { get; set; }

        public string Comment { get; set; }

        public Guid OrganizationId { get; set; }

        public Guid ProductId { get; set; }

        public Guid ProductVersionId { get; set; }

        public ModelState ModelState { get; set; }

        public Guid TargetFileId { get; set; }

        public Guid RootConfigurationId { get; set; }

        public Guid? ParentConfigurationId { get; set; }

        [ForeignKey("TemplateConfiguration")]
        public Guid? TemplateConfigurationId { get; set; }
        public virtual Configuration TemplateConfiguration { get; set; }

        [ForeignKey("ComponentDefinition")]
        public Guid ComponentDefinitionId { get; set; }
        public virtual ComponentDefinition ComponentDefinition { get; set; }

        public Guid AppBundleId { get; set; }
        public virtual AppBundle AppBundle { get; set; }

        public virtual ICollection<ParameterDefinition> ParameterDefinitions { get; set; }
        public DateTime Created { get; set; }
        
    }
}
