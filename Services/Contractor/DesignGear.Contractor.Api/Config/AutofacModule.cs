using Autofac;
using DesignGear.Contractor.Core.Data;
using DesignGear.Contractor.Core.Data.Entity;
using DesignGear.Contractor.Core.Services;
using DesignGear.Contractor.Core.Services.Interfaces;
using DesignGear.Contracts.Communicators;
using DesignGear.Contracts.Communicators.Interfaces;

namespace DesignGear.Contractor.Api.Config
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
            builder.RegisterType<ConfigManagerCommunicator>().As<IConfigManagerCommunicator>().InstancePerLifetimeScope();

            //services
            builder.RegisterType<AppBundleService>().As<IAppBundleService>().InstancePerLifetimeScope();
            builder.RegisterType<AuthenticationService>().As<IAuthenticationService>().InstancePerLifetimeScope();
            builder.RegisterType<ConfigurationService>().As<IConfigurationService>().InstancePerLifetimeScope();
            builder.RegisterType<OrganizationService>().As<IOrganizationService>().InstancePerLifetimeScope();
            builder.RegisterType<ProductService>().As<IProductService>().InstancePerLifetimeScope();
            builder.RegisterType<ProductVersionService>().As<IProductVersionService>().InstancePerLifetimeScope();
            builder.RegisterType<TariffService>().As<ITariffService>().InstancePerLifetimeScope();
            builder.RegisterType<UserService>().As<IUserService>().InstancePerLifetimeScope();
        }
    }
}
