using Autofac;
using DesignGear.ConfigManager.Core.Data;
using DesignGear.ConfigManager.Core.Jobs;
using DesignGear.ConfigManager.Core.Services;
using DesignGear.ConfigManager.Core.Services.Interfaces;
using DesignGear.ConfigManager.Core.Storage;
using DesignGear.ConfigManager.Core.Storage.Interfaces;
using DesignGear.Contracts.Communicators;
using DesignGear.Contracts.Communicators.Interfaces;

namespace DesignGear.ConfigManager.Api.Config
{
    public class AutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            //datacontext
            builder.RegisterType<DataReader>().AsSelf().InstancePerLifetimeScope();
            builder.RegisterType<DataEditor>().AsSelf().InstancePerLifetimeScope();
            builder.RegisterType<DataAccessor>().AsSelf().InstancePerLifetimeScope();

            //communicators
            //builder.RegisterType<ConfigManagerCommunicator>().As<IConfigManagerCommunicator>().InstancePerLifetimeScope();

            //file storage
            builder.RegisterType<ConfigurationFileStorage>().As<IConfigurationFileStorage>().InstancePerLifetimeScope();

            //services
            builder.RegisterType<AppBundleService>().As<IAppBundleService>().InstancePerLifetimeScope();
            builder.RegisterType<ConfigurationService>().As<IConfigurationService>().InstancePerLifetimeScope();

            //jobs
            builder.RegisterType<ConfigurationPushingJob>().AsSelf().InstancePerLifetimeScope();
            builder.RegisterType<ConfigurationPullingJob>().AsSelf().InstancePerLifetimeScope();
        }
    }
}
