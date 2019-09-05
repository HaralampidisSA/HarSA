using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Net.Http;

namespace HarSA.AspNetCore.Logging.Json
{
    internal class JsonLoggerProvider : ILoggerProvider
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly string applicationName;
        private readonly HttpClient httpClient;
        private readonly string apiServiceUrl;

        public JsonLoggerProvider(IHttpContextAccessor httpContextAccessor, string applicationName, string apiServiceUrl)
        {
            this.httpContextAccessor = httpContextAccessor;
            this.applicationName = applicationName;
            httpClient = new HttpClient();
            this.apiServiceUrl = apiServiceUrl;
        }

        public ILogger CreateLogger(string categoryName)
        {
            return new JsonLogger(categoryName, applicationName, httpClient, httpContextAccessor, apiServiceUrl);
        }

        public void Dispose()
        {
            httpClient.Dispose();
        }
    }
}