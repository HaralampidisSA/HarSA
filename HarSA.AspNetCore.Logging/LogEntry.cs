using System;

namespace HarSA.AspNetCore.Logging
{
    public class LogEntry
    {
        public DateTime Date { get; set; }
        public string Level { get; set; }
        public string Message { get; set; }
        public string Username { get; set; }
        public string Url { get; set; }
        public string Logger { get; set; }
        public string Exception { get; set; }
        public string Application { get; set; }
    }
}
