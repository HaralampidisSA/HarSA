using Autofac;
using Microsoft.Extensions.Configuration;

namespace HarSA.AspNetCore.Extensions
{
    public static class ContainerBuilderExtensions
    {
        public static void ConfigureContainer(this ContainerBuilder builder, IConfiguration configuration)
        {
            EngineContext.Current.ConfigureContainer(builder, configuration);
        }
    }
}