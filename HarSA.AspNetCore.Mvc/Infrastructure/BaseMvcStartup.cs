using FluentValidation.AspNetCore;
using HarSA.Startups;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HarSA.AspNetCore.Mvc.Infrastructure
{
    public abstract class BaseMvcStartup : IAppStartup
    {
        public virtual int Order => 10000;

        public void Configure(IApplicationBuilder application)
        {
            application.UseMvcWithDefaultRoute();
        }

        public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2).AddFluentValidation();
        }
    }
}