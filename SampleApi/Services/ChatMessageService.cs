using HarSA.EntityFrameworkCore.Application;
using HarSA.EntityFrameworkCore.Repositories;
using SampleApi.Models;

namespace SampleApi.Services
{
    public class ChatMessageService : CrudService<ChatMessage>, IChatMessageService
    {
        public ChatMessageService(IRepo<ChatMessage> repo) : base(repo)
        {
        }
    }
}
