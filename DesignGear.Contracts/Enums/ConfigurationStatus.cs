using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignGear.Contracts.Enums {
    public enum ConfigurationStatus {
        InQueue,
        InProcess,
        /*Ограничить кол-во запросов для заявок на конфигурацию с данным статусом (или прекратить делать запросы совсем или увеличивать интревал между запросами)*/
        ServiceUnavailableError,
        IncorrectRequestError,
        InvalidConfiguration,
        Ready
    }
}
