using HarSA.Logger.Client.Configurations;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Concurrent;

namespace HarSA.Logger.Client
{
    public class HarLoggerProvider : ILoggerProvider
    {
        private readonly IOptionsMonitor<HarLoggerOptions> _config;
        private readonly ConcurrentDictionary<string, HarLogger> _loggers;
        private readonly HarLoggerProcessor _messageQueue;
        private readonly IHttpContextAccessor _httpContext;

        private IDisposable _optionsReloadToken;


        public HarLoggerProvider(IOptionsMonitor<HarLoggerOptions> options, IHttpContextAccessor httpContextAccessor)
        {
            _config = options;
            _loggers = new ConcurrentDictionary<string, HarLogger>();
            _messageQueue = new HarLoggerProcessor();
            _httpContext = httpContextAccessor;
        }

        public ILogger CreateLogger(string categoryName)
        {
            return _loggers.GetOrAdd(categoryName, loggerName => new HarLogger(loggerName, _messageQueue, _httpContext)
            {
                Options = _config.CurrentValue
            });
        }

        public void Dispose()
        {
            _optionsReloadToken?.Dispose();
            _messageQueue.Dispose();
        }
    }
}
