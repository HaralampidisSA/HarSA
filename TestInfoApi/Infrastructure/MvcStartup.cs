using HarSA.AspNetCore.Api.Infrastructure;
using Microsoft.AspNetCore.Builder;

namespace TestInfoApi.Infrastructure
{
    public class MvcStartup : BaseApiMvcStartup
    {
        public override void Configure(IApplicationBuilder application)
        {
            application.UseRouting();

            base.Configure(application);
        }
    }
}
