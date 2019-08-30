using HarSA.Startups;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HarSA.AspNetCore.Api.Infrastructure
{
    public abstract class BaseCorsStartup : IAppStartup
    {
        public virtual int Order => 9000;

        public virtual void Configure(IApplicationBuilder application)
        {
            application.UseCors("All");
        }

        public virtual void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("All", builder =>
                {
                    builder.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin();
                });
            });
        }
    }
}
