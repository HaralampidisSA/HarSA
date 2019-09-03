using Autofac;
using HarSA.AspNetCore.Mvc.Notifications;
using HarSA.Configurations;
using HarSA.Dependency;
using Microsoft.Extensions.Configuration;

namespace HarSA.AspNetCore.Mvc.Infrastructure
{
    public class NotificationRegistrar : IDependencyRegistrar
    {
        public int Order => 9000;

        public void Register(ContainerBuilder builder, ITypeFinder typeFinder, HarConfig config, IConfiguration configuration)
        {
            builder.RegisterType<NotificationService>().As<INotificationService>().InstancePerLifetimeScope();
        }
    }
}
