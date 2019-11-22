using Autofac;
using HarSA.Startups;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace SampleApi.Infrastructure
{
    public class FileServerStartup : IAppStartup
    {
        public int Order => 0;

        public void Configure(IApplicationBuilder application)
        {
            application.UseStaticFiles();
        }

        public void ConfigureContainer(ContainerBuilder containerBuilder, IConfiguration configuration)
        {

        }

        public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {

        }
    }
}
