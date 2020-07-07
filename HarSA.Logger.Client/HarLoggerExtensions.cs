using HarSA.Logger.Client.Configurations;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Configuration;
using System;

namespace HarSA.Logger.Client
{
    public static class HarLoggerExtensions
    {
        public static ILoggingBuilder AddHarLogger(this ILoggingBuilder builder, Action<HarLoggerOptions> configure)
        {
            if (configure == null)
            {
                throw new ArgumentNullException(nameof(configure));
            }

            builder.AddHarLogger();
            builder.Services.Configure(configure);

            return builder;
        }

        private static ILoggingBuilder AddHarLogger(this ILoggingBuilder builder)
        {
            builder.AddConfiguration();
            builder.Services.TryAddEnumerable(ServiceDescriptor.Singleton<ILoggerProvider, HarLoggerProvider>());
            return builder;
        }
    }
}
