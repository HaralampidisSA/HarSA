using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;

namespace HarSA.AspNetCore.Logging.Json
{
    internal class JsonLogger : ILogger
    {
        private readonly string categoryName;
        private readonly string applicationName;
        private readonly HttpClient httpClient;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly string apiServiceUrl;

        public JsonLogger(string categoryName, string applicationName, HttpClient httpClient, IHttpContextAccessor httpContextAccessor, string apiServiceUrl)
        {
            this.categoryName = categoryName;
            this.applicationName = applicationName;
            this.httpClient = httpClient;
            this.httpContextAccessor = httpContextAccessor;
            this.apiServiceUrl = apiServiceUrl;
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
                return;

            if (formatter == null)
                throw new ArgumentNullException(nameof(formatter));

            var message = formatter(state, exception);

            if (!string.IsNullOrEmpty(message) || exception != null)
            {
                var entry = CreateLogEntry(logLevel, categoryName, eventId.Id, message, exception);
                WriteLogEntry(entry);
            }
        }

        private void WriteLogEntry(LogEntry entry)
        {
            var content = new StringContent(JsonConvert.SerializeObject(entry), Encoding.UTF8, "application/json");
            _ = httpClient.PostAsync(apiServiceUrl, content).GetAwaiter().GetResult();
        }

        private LogEntry CreateLogEntry(LogLevel logLevel, string logName, int eventId, string message, Exception exception)
        {
            var logEntry = new LogEntry
            {
                Application = applicationName,
                Date = DateTime.Now,
                Exception = GetLogException(exception),
                Level = GetLogLevelString(logLevel),
                Logger = logName,
                Message = message
            };

            if (httpContextAccessor.HttpContext != null)
            {
                var user = httpContextAccessor.HttpContext.User?.Identity.Name;
                var url = httpContextAccessor.HttpContext.Request.Path.Value;

                logEntry.Username = user ?? "";
                logEntry.Url = url ?? "";
            }

            return logEntry;
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