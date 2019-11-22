using HarSA.AspNetCore.Api.Infrastructure;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.OpenApi.Models;

namespace TestInfoApi.Infrastructure
{
    public class SwashbuckleStartup : BaseSwashbuckleStartup
    {
        public SwashbuckleStartup() : base("TestInfoApi.xml")
        {
        }

        public override OpenApiInfo CreateInfoForApiVersion(ApiVersionDescription apiVersionDescription)
        {
            var info = new OpenApiInfo
            {
                Contact = new OpenApiContact { Name = "Αλέξανδρος Νάκος", Email = "a.nakos@haralampidis.gr" },
                Description = "Διαχείριση OPC Servers",
                Title = $"HarSA.OPC API {apiVersionDescription.ApiVersion}",
                License = new OpenApiLicense { Name = "ΧΑΡΑΛΑΜΠΙΔΗΣ Α.Ε." },
                Version = apiVersionDescription.ApiVersion.ToString()
            };

            if (apiVersionDescription.IsDeprecated)
            {
                info.Description += "This API version has been deprecated. Better start using the upgraded version.";
            }

            return info;
        }
    }
}
