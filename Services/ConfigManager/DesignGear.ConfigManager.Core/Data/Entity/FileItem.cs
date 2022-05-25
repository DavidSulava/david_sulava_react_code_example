using DesignGear.ConfigManager.Core.Data.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DesignGear.ConfigManager.Core.Data.Entity
{
    public class FileItem : IGenerateUid
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public Guid ConfigurationId { get; set; }

        [StringLength(500)]
        public string FilePath { get; set; }

        public bool IsModelFile { get; set; }

        [NotMapped]
        public int FileId { get; set; }
    }
}
