using HarSA.AspNetCore.Logging.Json;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;

namespace SampleLogWebApp
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddHttpContextAccessor();

            services
                .AddAuthentication(o =>
                {
                    o.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                    o.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
                })
                .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
                {
                    options.AccessDeniedPath = new PathString("/AccessDenied");
                })
                .AddOpenIdConnect(OpenIdConnectDefaults.AuthenticationScheme, o =>
                {
                    o.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                    o.Authority = "http://10.116.84.254:5901";
                    o.RequireHttpsMetadata = false;

                    o.ClientId = "hybrid.client";
                    o.ClientSecret = "secret";

                    o.ResponseType = "code id_token";

                    o.SaveTokens = true;
                    o.GetClaimsFromUserInfoEndpoint = true;

                    o.Scope.Add("har.signalr.api");
                    o.Scope.Add("har.hangfire.server.api");
                    o.Scope.Add("har.softone.api");
                    o.Scope.Add("offline_access");
                });


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            return services.BuildServiceProvider();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseExceptionHandler("/Home/Error");

            var factory = app.ApplicationServices.GetRequiredService<ILoggerFactory>();
            var httpContextAccessor = app.ApplicationServices.GetRequiredService<IHttpContextAccessor>();
            factory.AddJsonLogger(httpContextAccessor, "SampleLog", "http://localhost:58887/v1/Log/AddApplicationLog");

            app.UseAuthentication();

            app.UseMvcWithDefaultRoute();
        }
    }
}
