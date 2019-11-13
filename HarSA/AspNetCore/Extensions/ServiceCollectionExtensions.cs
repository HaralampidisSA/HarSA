using HarSA.Configurations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace HarSA.AspNetCore.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void ConfigureApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            // add config
            var config = services.ConfigureStartupConfig<HarConfig>(configuration.GetSection("HarConfig"));
            services.AddHttpContextAccessor();

            // create configure engine
            var engine = EngineContext.Create();
            engine.ConfigureEngineServices(services, configuration, config);
        }

        public static TConfig ConfigureStartupConfig<TConfig>(this IServiceCollection services, IConfiguration configuration) where TConfig : class, new()
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));

            if (configuration == null)
                throw new ArgumentNullException(nameof(configuration));

            //create instance of config
            var config = new TConfig();

            //bind it to the appropriate section of configuration
            configuration.Bind(config);

            //and register it as a service
            services.AddSingleton(config);

            return config;
        }
    }
}