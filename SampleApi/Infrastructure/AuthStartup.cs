using Autofac;
using HarSA.Startups;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace SampleApi.Infrastructure
{
    public class AuthStartup : IAppStartup
    {
        public int Order => 500;

        public void Configure(IApplicationBuilder application)
        {
            application.UseAuthentication();
            application.UseAuthorization();
        }

        public void ConfigureContainer(ContainerBuilder containerBuilder, IConfiguration configuration)
        {
        }

        public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            var openIdAuthority = configuration.GetValue<string>("OpenIdAuthority");
            if (string.IsNullOrEmpty(openIdAuthority))
            {
                throw new ArgumentNullException("OpenId Authority is not provided in appsettings. Key: OpenIdAuthority.");
            }

            var clientId = configuration.GetValue<string>("ClientId");
            if (string.IsNullOrEmpty(clientId))
            {
                throw new ArgumentNullException("ClientId is not provided in appsettings. Key: ClientId.");
            }

            var clientSecret = configuration.GetValue<string>("ClientSecret");
            if (string.IsNullOrEmpty(clientSecret))
            {
                throw new ArgumentNullException("ClientSecret is not provided in appsettings. Key: ClientSecret.");
            }

            services
                .AddAuthentication(o =>
                {
                    o.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                    o.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
                })
                .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, o =>
                {
                    o.AccessDeniedPath = new PathString("/Home/AccessDenied");
                })
                .AddOpenIdConnect(OpenIdConnectDefaults.AuthenticationScheme, o =>
                {
                    o.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                    o.Authority = openIdAuthority;
                    o.RequireHttpsMetadata = false;

                    o.ClientId = clientId;
                    o.ClientSecret = clientSecret;

                    o.ResponseType = "code id_token";

                    o.SaveTokens = true;
                    o.GetClaimsFromUserInfoEndpoint = true;

                    o.Scope.Add("har.signalr.api");
                    o.Scope.Add("har.hangfire.server.api");
                    o.Scope.Add("har.softone.api");
                    o.Scope.Add("offline_access");
                    o.Scope.Add("har.project.manager.api");
                    o.Scope.Add("har.logger.api");
                });
        }
    }
}