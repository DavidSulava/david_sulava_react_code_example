using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignGear.Contracts.Models.ServerManager.Derivative
{
    public class VmStatusJob
    {
        public string Status { get; set; }
        public IEnumerable<Stream> SvfFiles { get; set; }
    }
}
