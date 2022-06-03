using DesignGear.Contractor.Core.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignGear.Contractor.Core.Data.Entity {
    public class ProductVersionPreview : IGenerateUid, ICreated {
        public Guid Id { get; set; }
        [StringLength(300)]
        public string FileName { get; set; }
        [StringLength(100)]
        public string ContentType { get; set; }
        public byte[] Content { get; set; }
        public Guid ProductVersionId { get; set; }
        public virtual ProductVersion ProductVersion { get; set; }
        public DateTime Created { get; set; }
    }
}
