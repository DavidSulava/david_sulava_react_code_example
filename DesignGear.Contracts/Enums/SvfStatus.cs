using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignGear.Contracts.Enums {
    [Flags]
    public enum SvfStatus {
        InQueue,
        InProcess,
        ServiceUnavailableError,
        IncorrectRequestError,
        Ready
    }
}
