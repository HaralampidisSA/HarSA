using HarSA.Domain;
using System;

namespace SampleApi.Models
{
    public class OnlineClient : BaseEntity
    {
        public string UserId { get; set; }
        public DateTime ConnectTime { get; set; }
        public string Username { get; set; }
        public string ConnectionId { get; set; }

    }
}
