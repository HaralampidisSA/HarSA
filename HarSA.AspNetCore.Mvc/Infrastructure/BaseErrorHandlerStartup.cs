using HarSA.Startups;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HarSA.AspNetCore.Mvc.Infrastructure
{
    public abstract class BaseErrorHandlerStartup : IAppStartup
    {
        public virtual int Order => 0;

        public virtual void Configure(IApplicationBuilder application)
        {
            var enviroment = EngineContext.Current.Resolve<IHostingEnvironment>();
            var useDetailedExceptionPage = enviroment.IsDevelopment();

            if (useDetailedExceptionPage)
            {
                application.UseDeveloperExceptionPage();
            }
            else
            {
                application.UseExceptionHandler("/Home/Error");
            }

            // log errors

        }

        public virtual void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {

        }
    }
}
