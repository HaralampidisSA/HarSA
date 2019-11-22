using HarSA.EntityFrameworkCore.Application;
using HarSA.EntityFrameworkCore.Repositories;
using SampleApi.Models;

namespace SampleApi.Services
{
    public class OnlineClientService : CrudService<OnlineClient>, IOnlineClientService
    {
        public OnlineClientService(IRepo<OnlineClient> repo) : base(repo)
        {
        }

        public OnlineClient FindOnlineClientByConnectionId(string connectionId)
        {
            return Repository.First(w => connectionId.Equals(w.ConnectionId));
        }
    }
}