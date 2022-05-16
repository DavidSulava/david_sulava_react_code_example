using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignGear.Contracts.Enums {
    public enum ConfigurationStatus {
        InQueue,
        InProcess,
        ServiceUnavailableError,
        IncorrectRequestError,
        Ready
    }
}
