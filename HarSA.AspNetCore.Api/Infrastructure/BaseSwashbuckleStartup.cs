using HarSA.Startups;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.IO;

namespace HarSA.AspNetCore.Api.Infrastructure
{
    public abstract class SwashbuckleBaseStartup : IAppStartup
    {
        private readonly string _xmlFile;

        protected SwashbuckleBaseStartup(string xmlFile)
        {
            _xmlFile = xmlFile;
        }

        public int Order => 10000;

        public void Configure(IApplicationBuilder application)
        {
            var provider = application.ApplicationServices.GetRequiredService<IApiVersionDescriptionProvider>();

            application.UseSwagger();
            application.UseSwaggerUI(options =>
            {
                foreach (var description in provider.ApiVersionDescriptions)
                {
                    options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant());
                }
            });
        }

        public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddSwaggerGen(options =>
            {
                var provider = services.BuildServiceProvider().GetRequiredService<IApiVersionDescriptionProvider>();

                foreach (var description in provider.ApiVersionDescriptions)
                {
                    options.SwaggerDoc(description.GroupName, CreateInfoForApiVersion(description));

                    // integrate xml comments.
                    var xmlPath = Path.Combine(AppContext.BaseDirectory, _xmlFile);

                    options.IncludeXmlComments(xmlPath);
                }
            });
        }

        public abstract Info CreateInfoForApiVersion(ApiVersionDescription apiVersionDescription);
    }
}
