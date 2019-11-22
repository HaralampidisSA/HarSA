using HarSA.EntityFrameworkCore.Application;
using SampleApi.Models;

namespace SampleApi.Services
{
    public interface IChatMessageService : ICrudService<ChatMessage>
    {
    }
}
