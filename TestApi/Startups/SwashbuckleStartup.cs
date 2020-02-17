using HarSA.AspNetCore.Api.Infrastructure;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.OpenApi.Models;

namespace TestApi.Startups
{
    public class SwashbuckleStartup : BaseSwashbuckleStartup
    {
        public SwashbuckleStartup() : base("TestApi.xml")
        {
        }

        public override OpenApiInfo CreateInfoForApiVersion(ApiVersionDescription apiVersionDescription)
        {
            return new OpenApiInfo();
        }
    }
}
