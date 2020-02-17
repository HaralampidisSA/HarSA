using HarSA.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace TestApi
{
    public class Startup : BaseStartup
    {
        public Startup(IWebHostEnvironment environment) : base(environment)
        {
        }
    }
}
