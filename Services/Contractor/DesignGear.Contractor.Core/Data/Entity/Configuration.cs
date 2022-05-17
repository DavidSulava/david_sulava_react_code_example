using DesignGear.Common.Enums;
using DesignGear.Contractor.Core.Data.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DesignGear.Contractor.Core.Data.Entity
{
    public class Configuration : IGenerateUid
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [StringLength(300)]
        public string Name { get; set; }

        public string Comment { get; set; }

        public ModelState ModelState { get; set; }

        [ForeignKey("ProductVersion")]
        public Guid ProductVersionId { get; set; }
        public virtual ProductVersion ProductVersion { get; set; }

        public Guid TargetFileId { get; set; }

        public Guid AppBundleId { get; set; }
        public virtual AppBundle AppBundle { get; set; }

        public virtual ICollection<ParameterDefinition> ParameterDefinitions { get; set; }
    }
}
