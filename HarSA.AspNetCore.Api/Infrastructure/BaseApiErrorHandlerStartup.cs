using Autofac;
using HarSA.Startups;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Threading.Tasks;

namespace HarSA.AspNetCore.Api.Infrastructure
{
    public abstract class BaseApiErrorHandlerStartup : IAppStartup
    {
        public virtual int Order => 0;

        public virtual void Configure(IApplicationBuilder application)
        {
            application.UseMiddleware<ExceptionMiddleware>();
        }

        public virtual void ConfigureContainer(ContainerBuilder containerBuilder, IConfiguration configuration)
        {

        }

        public virtual void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {

        }


    }

    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public ExceptionMiddleware(RequestDelegate next,
            ILoggerFactory loggerFactory)
        {
            _next = next;
            _logger = loggerFactory.CreateLogger("ExceptionsHandler");
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            if (_logger != null)
            {
                _logger.LogError(exception, exception.Message);
            }

            var response = context.Response;

            response.StatusCode = (int)HttpStatusCode.InternalServerError;
            response.ContentType = "application/json";
            await response.WriteAsync(JsonConvert.SerializeObject(exception));
        }
    }
}
