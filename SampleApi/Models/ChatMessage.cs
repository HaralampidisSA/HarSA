using HarSA.Domain;

namespace SampleApi.Models
{
    public class ChatMessage : BaseEntity
    {
        public string FromUserId { get; set; }
        public string ToUserId { get; set; }
        public string Message { get; set; }
    }
}