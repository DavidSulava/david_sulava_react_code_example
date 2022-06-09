using DesignGear.ConfigManager.Core.Jobs.Interfaces;
using DesignGear.ConfigManager.Core.Services.Interfaces;
using DesignGear.ConfigManager.Core.Storage.Interfaces;
using DesignGear.Contracts.Communicators.Interfaces;
using DesignGear.Contracts.Dto.ConfigManager;
using DesignGear.Contracts.Enums;

namespace DesignGear.ConfigManager.Core.Jobs
{
    public class ConfigurationPullingJob : IJob
    {
        private readonly IConfigurationService _configurationService;
        private readonly IServerManagerCommunicator _serverManagerService;
        private readonly IConfigurationFileStorage _configurationFileStorage;

        public ConfigurationPullingJob(IConfigurationService configurationService,
            IServerManagerCommunicator serverManagerService,
            IConfigurationFileStorage configurationFileStorage)
        {
            _configurationService = configurationService ?? throw new ArgumentNullException(nameof(configurationService));
            _serverManagerService = serverManagerService ?? throw new ArgumentNullException(nameof(serverManagerService));
            _configurationFileStorage = configurationFileStorage ?? throw new ArgumentNullException(nameof(configurationFileStorage));
        }

        public void Do()
        {
            /*
             * Получаем список конфигураций со статусом InProcess
             */
            var configurations = _configurationService.GetConfigurationListAsync(new ConfigurationFilterDto
            {
                Status = ConfigurationStatus.InProcess
            }).Result;

            /*
             * Для каждой конфигурации делаем запрос о статусе, получаем результат если готово и сохраняем его
             * через IConfigurationService. Фиксируем статус Ready для тех конфигураций, который в итоге пересчитаны
             */

            foreach (var configuration in configurations)
            {
                try
                {
                    var result = _serverManagerService.CheckStatusJobAsync(configuration.WorkItemId).Result;
                    if (result == ConfigurationStatus.Ready)
                    {
                        var modelStream = _serverManagerService.DownloadModelAsync(configuration.WorkItemUrl).Result;
                        _configurationService.UpdateConfigurationAsync(new ConfigurationStatusUpdateDto()
                        {
                            ConfigurationId = configuration.Id,
                            Status = ConfigurationStatus.Ready,
                            ConfigurationPackage = modelStream
                        }).Wait();
                    }
                }
                catch (Exception ex)
                {
                }
            }

            /*
             * В таком же русле необходимо сделать проверку запроса и получение результата по svf
             */
            configurations = _configurationService.GetConfigurationListAsync(new ConfigurationFilterDto
            {
                SvfStatus = SvfStatus.InProcess
            }).Result;

            foreach (var configuration in configurations)
            {
                try
                {
                    var result = _serverManagerService.CheckSvfStatusJobAsync(configuration.URN).Result;
                    if (result == SvfStatus.Ready)
                    {
                        var file = _serverManagerService.DownloadSvfAsync(configuration.URN).Result;
                        if (file != null)
                        {
                            _configurationFileStorage.SaveSvfAsync(configuration.ProductVersionId, configuration.Id, file).Wait();
                            _configurationService.UpdateSvfStatus(new ConfigurationSvfStatusUpdateDto
                            {
                                ConfigurationId = configuration.Id,
                                SvfStatus = SvfStatus.Ready,
                                URN = configuration.URN
                            });
                        }
                    }
                }
                catch (Exception ex)
                {
                }
            }
        }
    }
}
