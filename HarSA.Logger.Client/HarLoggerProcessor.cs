using HarSA.Logger.Client.Models;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;

namespace HarSA.Logger.Client
{
    public class HarLoggerProcessor : IDisposable
    {
        private readonly HttpClient _httpClient;

        public HarLoggerProcessor()
        {
            _httpClient = new HttpClient();
        }

        public void PostMessage(LogMessageEntry logMessageEntry, string postUrl)
        {
            var postContent = new StringContent(JsonConvert.SerializeObject(logMessageEntry), Encoding.UTF8, "application/json");
            _ = _httpClient.PostAsync(postUrl, postContent).GetAwaiter().GetResult();
        }

        public void Dispose()
        {
            _httpClient?.Dispose();
        }
    }
}
