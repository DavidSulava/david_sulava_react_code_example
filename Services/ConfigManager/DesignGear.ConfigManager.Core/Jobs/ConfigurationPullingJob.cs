using DesignGear.ConfigManager.Core.Jobs.Interfaces;
using DesignGear.ConfigManager.Core.Services.Interfaces;
using DesignGear.Contracts.Dto.ConfigManager;
using DesignGear.Contracts.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignGear.ConfigManager.Core.Jobs {
    public class ConfigurationPullingJob : IJob {
        private readonly IConfigurationService _configurationService;

        public ConfigurationPullingJob(IConfigurationService configurationService) {
            _configurationService = configurationService ?? throw new ArgumentNullException(nameof(configurationService));
        }

        public void Do() {
            /*
             * Получаем список конфигураций со статусом InProcess
             */
            var configurations = _configurationService.GetConfigurationListAsync(new ConfigurationFilterDto {
                Status = ConfigurationStatus.InProcess
            }).Result;

            /*
             * Для каждой конфигурации делаем запрос о статусе, получаем результат если готово и сохраняем его
             * через IConfigurationService. Фиксируем статус Ready для тех конфигураций, который в итоге пересчитаны
             */
            foreach (var configuration in configurations) {
            
            }

            /*
             * В таком же русле необходимо сделать проверку запроса и получение результата по svf
             */
        }
    }
}
