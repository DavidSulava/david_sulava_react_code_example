using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignGear.Contracts.Models.Contractor
{
    public class Tariff
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid TariffsId { get; set; }

        public string Name { get; set; }
    }
}

