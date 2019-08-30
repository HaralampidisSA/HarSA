using HarSA.Startups;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
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

        public virtual void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {

        }


    }

    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
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
            var response = context.Response;

            response.StatusCode = (int)HttpStatusCode.InternalServerError;
            response.ContentType = "application/json";
            await response.WriteAsync(JsonConvert.SerializeObject(exception));
        }
    }
}
