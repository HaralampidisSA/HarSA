using HarSA.Logger.Client.Configurations;
using HarSA.Logger.Client.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;

namespace HarSA.Logger.Client
{
    public class HarLogger : ILogger
    {
        private readonly string _loggerName;
        private readonly HarLoggerProcessor _processor;
        private readonly IHttpContextAccessor _httpAccessor;

        public HarLoggerOptions Options { get; set; }

        public HarLogger(string loggerName, HarLoggerProcessor processor, IHttpContextAccessor httpContextAccessor)
        {
            if (loggerName == null)
            {
                throw new ArgumentNullException(nameof(loggerName));
            }

            _loggerName = loggerName;
            _processor = processor;
            _httpAccessor = httpContextAccessor;
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            return null;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return logLevel != LogLevel.None;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            if (!IsEnabled(logLevel))
            {
                return;
            }

            if (formatter == null)
            {
                throw new ArgumentNullException(nameof(formatter));
            }

            var message = formatter(state, exception);
            if (!string.IsNullOrEmpty(message) || exception != null)
            {
                WriteMessage(logLevel, _loggerName, eventId.Id, message, exception);
            }
        }

        public void WriteMessage(LogLevel logLevel, string logName, int eventId, string message, Exception exception)
        {
            var entry = new LogMessageEntry
            {
                Application = Options.ApplicationName,
                Date = DateTime.Now,
                Exception = GetLogException(exception),
                Level = GetLogLevelString(logLevel),
                Logger = logName,
                Message = message,
            };

            if (_httpAccessor.HttpContext != null)
            {
                var user = _httpAccessor.HttpContext.User?.Identity.Name;
                var url = _httpAccessor.HttpContext.Request.Path.Value;

                entry.Username = user ?? "";
                entry.Url = url ?? "";
            }

            _processor.PostMessage(entry, Options.PostUrl);
        }

        private static string GetLogLevelString(LogLevel logLevel)
        {
            switch (logLevel)
            {
                case LogLevel.Trace:
                    return "trce";

                case LogLevel.Debug:
                    return "dbug";

                case LogLevel.Information:
                    return "info";

                case LogLevel.Warning:
                    return "warn";

                case LogLevel.Error:
                    return "fail";

                case LogLevel.Critical:
                    return "crit";

                default:
                    throw new ArgumentOutOfRangeException(nameof(logLevel));
            }
        }

        private static string GetLogException(Exception exception)
        {
            var result = string.Empty;
            if (exception != null)
                result = exception.ToString();

            return result;
        }
    }
}