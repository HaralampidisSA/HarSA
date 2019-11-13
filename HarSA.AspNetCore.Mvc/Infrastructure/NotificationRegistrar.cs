using Autofac;
using HarSA.AspNetCore.Mvc.Notifications;
using HarSA.Startups;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HarSA.AspNetCore.Mvc.Infrastructure
{
    public class NotificationRegistrar : IAppStartup
    {
        public int Order => 9000;

        public void Configure(IApplicationBuilder application)
        {
        }

        public void ConfigureContainer(ContainerBuilder containerBuilder, IConfiguration configuration)
        {
            containerBuilder.RegisterType<NotificationService>().As<INotificationService>().InstancePerLifetimeScope();
        }

        public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
        }
    }
}