using DesignGear.ConfigManager.Core.Data.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DesignGear.ConfigManager.Core.Data.Entity
{
    public class ComponentDefinition : IGenerateUid, ICreated
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        [StringLength(200)]
        public string UniqueId { get; set; }

        [StringLength(300)]
        public string Name { get; set; }
        public Guid TemplateConfigurationId { get; set; }
        public virtual ICollection<Configuration> Configurations { get; set; }
        public DateTime Created { get; set; }

        public Guid OrganizationId { get; set; }

        public Guid ProductId { get; set; }

        public Guid ProductVersionId { get; set; }

        public Guid AppBundleId { get; set; }
        public virtual AppBundle AppBundle { get; set; }
    }
}
