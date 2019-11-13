using Autofac;
using HarSA.Startups;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SampleApi.Hubs;

namespace SampleApi.Infrastructure
{
    public class SignalRStartup : IAppStartup
    {
        public int Order => 9000;

        public void Configure(IApplicationBuilder application)
        {
            application.UseEndpoints(endpoint =>
            {
                endpoint.MapHub<ChatHub>("/chat");
            });
        }

        public void ConfigureContainer(ContainerBuilder containerBuilder, IConfiguration configuration)
        {

        }

        public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddSignalR();
        }
    }
}
