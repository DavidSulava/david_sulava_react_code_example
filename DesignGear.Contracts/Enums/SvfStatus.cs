using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignGear.Contracts.Enums {
    [Flags]
    public enum SvfStatus {
        InQueue = 1,
        InProcess = 2,
        ServiceUnavailableError = 4,
        IncorrectRequestError = 8,
        Ready = 16
    }
}
