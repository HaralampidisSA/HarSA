using Autofac;
using HarSA.Startups;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Runtime.ExceptionServices;
using System.Threading.Tasks;

namespace HarSA.AspNetCore.Mvc.Infrastructure
{
    public abstract class BaseErrorHandlerStartup : IAppStartup
    {
        public virtual int Order => 0;

        public virtual void Configure(IApplicationBuilder application)
        {
            var enviroment = EngineContext.Current.Resolve<IWebHostEnvironment>();
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
            application.UseExceptionHandler(handler =>
            {
                handler.Run(context =>
                {
                    var exception = context.Features.Get<IExceptionHandlerFeature>()?.Error;
                    if (exception == null)
                    {
                        return Task.CompletedTask;
                    }

                    try
                    {
                        var logger = EngineContext.Current.Resolve<ILoggerFactory>().CreateLogger("ErrorHandler");
                        logger.LogError(exception, exception.Message);
                    }
                    finally
                    {
                        //rethrow the exception to show the error page
                        ExceptionDispatchInfo.Throw(exception);
                    }

                    return Task.CompletedTask;
                });
            });
        }

        public virtual void ConfigureContainer(ContainerBuilder containerBuilder, IConfiguration configuration)
        {
        }

        public virtual void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
        }
    }
}