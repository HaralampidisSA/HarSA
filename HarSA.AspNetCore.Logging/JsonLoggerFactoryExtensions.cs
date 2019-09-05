using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace HarSA.AspNetCore.Logging.Json
{
    public static class JsonLoggerFactoryExtensions
    {
        public static ILoggerFactory AddJsonLogger(this ILoggerFactory factory, IHttpContextAccessor httpContextAccessor, string applicationName, string apiServiceUrl)
        {
            factory.AddProvider(new JsonLoggerProvider(httpContextAccessor, applicationName, apiServiceUrl));

            return factory;
        }
    }
}
