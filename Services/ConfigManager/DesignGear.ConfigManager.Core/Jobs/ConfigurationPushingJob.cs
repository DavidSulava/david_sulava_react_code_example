using DesignGear.ConfigManager.Core.Jobs.Interfaces;
using DesignGear.ConfigManager.Core.Services.Interfaces;
using DesignGear.ConfigManager.Core.Storage.Interfaces;
using DesignGear.Contracts.Communicators.Interfaces;
using DesignGear.Contracts.Dto.ConfigManager;
using DesignGear.Contracts.Enums;

namespace DesignGear.ConfigManager.Core.Jobs
{
    public class ConfigurationPushingJob : IJob
    {
        private readonly IAppBundleService _appBundleService;
        private readonly IConfigurationService _configurationService;
        private readonly IServerManagerCommunicator _serverManagerService;
        private readonly IConfigurationFileStorage _configurationFileStorage;


        public ConfigurationPushingJob(IAppBundleService appBundleService,
            IConfigurationService configurationService,
            IServerManagerCommunicator serverManagerService,
            IConfigurationFileStorage configurationFileStorage)
        {
            _appBundleService = appBundleService ?? throw new ArgumentNullException(nameof(appBundleService));
            _configurationService = configurationService ?? throw new ArgumentNullException(nameof(configurationService));
            _serverManagerService = serverManagerService ?? throw new ArgumentNullException(nameof(serverManagerService));
            _configurationFileStorage = configurationFileStorage ?? throw new ArgumentNullException(nameof(configurationFileStorage));
        }

        public void Do()
        {
            /*
             * Получаем список конфигураций со статусом InQueue или ServiceUnavailableError
             */
            var configurations = _configurationService.GetConfigurationListAsync(new ConfigurationFilterDto
            {
                Status = ConfigurationStatus.InQueue// | ConfigurationStatus.ServiceUnavailableError
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
                    var packageFile = _configurationFileStorage.GetZipArchive(configuration.ProductVersionId, configuration.Id);
                    var appBundleFile = _appBundleService.GetAppBundleAsync(configuration.AppBundleId).Result;
                    if (packageFile != null && appBundleFile != null)
                    {
                        var result = _serverManagerService.ProcessModelAsync(appBundleFile.Content, packageFile).Result;
                        if (result != null)
                        {
                            _configurationService.UpdateModelStatus(new ConfigurationUpdateModelDto
                            {
                                ConfigurationId = configuration.Id,
                                Status = ConfigurationStatus.InProcess,
                                WorkItemId = result.Id,
                                WorkItemUrl = result.Url
                            });
                        }
                    }
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
                SvfStatus = SvfStatus.InQueue// | SvfStatus.ServiceUnavailableError
            }).Result;

            foreach (var configuration in configurations)
            {
                var packageFile = _configurationFileStorage.GetZipArchive(configuration.ProductVersionId, configuration.Id);
                if (packageFile != null)
                {
                    var urn = _serverManagerService.GetSvfAsync(packageFile, configuration.RootFileName).Result;
                    if (urn != null)
                    {
                        _configurationService.UpdateSvfStatus(new ConfigurationUpdateSvfDto
                        {
                            ConfigurationId = configuration.Id,
                            SvfStatus = SvfStatus.InProcess,
                            URN = urn
                        });
                    }
                }
            }
        }
    }
}
