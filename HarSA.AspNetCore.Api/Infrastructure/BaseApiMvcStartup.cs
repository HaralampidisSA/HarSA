using Autofac;
using FluentValidation.AspNetCore;
using HarSA.Startups;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace HarSA.AspNetCore.Api.Infrastructure
{
    public abstract class BaseApiMvcStartup : IAppStartup
    {
        public virtual int Order => 10000;

        public virtual void Configure(IApplicationBuilder application)
        {
            application.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        public virtual void ConfigureContainer(ContainerBuilder containerBuilder, IConfiguration configuration)
        {

        }

        public virtual void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddVersionedApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'VVV";
                options.SubstituteApiVersionInUrl = true;
            });



            services.AddControllers().AddFluentValidation();

            services.AddApiVersioning(options => options.ReportApiVersions = true);
        }
    }
}
