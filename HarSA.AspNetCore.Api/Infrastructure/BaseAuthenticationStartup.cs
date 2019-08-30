using HarSA.Startups;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace HarSA.AspNetCore.Api.Infrastructure
{
    public abstract class BaseAuthenticationStartup : IAppStartup
    {
        public virtual int Order => 8000;

        public virtual void Configure(IApplicationBuilder application)
        {
            application.UseAuthentication();
        }

        public virtual void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            var stsServer = configuration["StsServer"];
            if (string.IsNullOrEmpty(stsServer))
            {
                throw new ArgumentNullException("No stsServer is provided from appsettings.");
            }

            var stsAudience = configuration["StsAudience"];
            if (string.IsNullOrEmpty(stsAudience))
            {
                throw new ArgumentNullException("No stsAudience is provided from appsettings.");
            }

            services
                .AddAuthentication(o =>
                {
                    o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(o =>
                {
                    o.Authority = stsServer;
                    o.Audience = stsAudience;
                    o.SaveToken = true;
                    o.RequireHttpsMetadata = false;
                });
        }
    }
}
