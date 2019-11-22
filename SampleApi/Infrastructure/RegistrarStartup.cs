using Autofac;
using HarSA.Startups;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SampleApi.Services;

namespace SampleApi.Infrastructure
{
    public class RegistrarStartup : IAppStartup
    {
        public int Order => 1000;

        public void Configure(IApplicationBuilder application)
        {
        }

        public void ConfigureContainer(ContainerBuilder containerBuilder, IConfiguration configuration)
        {
            containerBuilder.RegisterType<OnlineClientService>().As<IOnlineClientService>().InstancePerLifetimeScope();
            containerBuilder.RegisterType<ChatMessageService>().As<IChatMessageService>().InstancePerLifetimeScope();
        }

        public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {

        }
    }
}