using HarSA.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace SampleApi
{
    public class Startup : BaseStartup
    {
        public Startup(IWebHostEnvironment environment) : base(environment)
        {
        }
    }
}
