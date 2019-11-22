using HarSA.EntityFrameworkCore.Application;
using SampleApi.Models;

namespace SampleApi.Services
{
    public interface IOnlineClientService : ICrudService<OnlineClient>
    {
        OnlineClient FindOnlineClientByConnectionId(string connectionId);
    }
}
