using DesignGear.ConfigManager.Core.Jobs.Interfaces;
using DesignGear.ConfigManager.Core.Services.Interfaces;
using DesignGear.ConfigManager.Core.Storage.Interfaces;
using DesignGear.Contracts.Dto.ConfigManager;
using DesignGear.Contracts.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignGear.ConfigManager.Core.Jobs {
    public class ConfigurationPushingJob : IJob {
        private readonly IConfigurationService _configurationService;
        

        public ConfigurationPushingJob(IConfigurationService configurationService) {
            _configurationService = configurationService ?? throw new ArgumentNullException(nameof(configurationService));
        }

        public void Do()
        {
            /*
             * Получаем список конфигураций со статусом InQueue или ServiceUnavailableError
             */
            var configurations = _configurationService.GetConfigurationListAsync(new ConfigurationFilterDto
            {
                Status = ConfigurationStatus.InQueue | ConfigurationStatus.ServiceUnavailableError
            }).Result;

            /*
             * Для каждой конфигурации формируем пакет вместе с пакетом AppBundle и отправляем в инвентор
             */
            foreach (var configuration in configurations)
            {
                try
                {
                    /*
                     * Здесь выполняем отправку в инвентор и меняем статус конфигурации на InProcess
                     */
                }
                catch (Exception ex)
                {
                    /*
                     * Здесь анализируем ошибку и в зависимости от ее типа устанавливаем статус конфигурации
                     * в ServiceUnavailableError или IncorrectRequestError. Также заполняем поле ErrorMessage,
                     * особенно в случае IncorrectRequestError
                     */
                }
            }

            /*
             * В таком же русле необходимо сделать отправку запроса на формирование svf
             */
            configurations = _configurationService.GetConfigurationListAsync(new ConfigurationFilterDto
            {
                SvfStatus = SvfStatus.InQueue | SvfStatus.ServiceUnavailableError
            }).Result;

            foreach (var configuration in configurations)
            {
            }
        }
    }
}
