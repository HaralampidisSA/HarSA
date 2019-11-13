using Autofac;
using FluentValidation.AspNetCore;
using HarSA.Startups;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HarSA.AspNetCore.Mvc.Infrastructure
{
    public abstract class BaseMvcStartup : IAppStartup
    {
        public virtual int Order => 10000;

        public virtual void Configure(IApplicationBuilder application)
        {
            application.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });
        }

        public virtual void ConfigureContainer(ContainerBuilder containerBuilder, IConfiguration configuration)
        {
        }

        public virtual void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllersWithViews().AddFluentValidation();
        }
    }
}