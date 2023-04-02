using Autofac;

namespace Connexity.BC.Tracking.Service.Adapters.Spi
{
    using Connexity.BC.Tracking.Service.Adapters.Spi.Configuration;
    using Connexity.BC.Tracking.Service.Adapters.Spi.Events;
    using Connexity.BC.Tracking.Service.Adapters.Spi.Store;

    public class SpiModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<RedisStoreAdapter>().AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterType<DaprNotifyAdapter>().AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterType<ServiceConfigAdapter>().AsImplementedInterfaces().InstancePerLifetimeScope();
        }
    }
}