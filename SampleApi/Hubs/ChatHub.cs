using Microsoft.AspNetCore.SignalR;
using SampleApi.Models;
using SampleApi.Services;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SampleApi.Hubs
{
    public class ChatHub : Hub
    {
        private readonly IOnlineClientService _clientService;

        public ChatHub(IOnlineClientService onlineClientService)
        {
            _clientService = onlineClientService;
        }

        public override async Task OnConnectedAsync()
        {
            var userId = Context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var username = Context.User.FindFirst(ClaimTypes.Name)?.Value;

            var client = new OnlineClient
            {
                ConnectTime = DateTime.Now,
                Username = username,
                ConnectionId = Context.ConnectionId,
                UserId = userId
            };

            _clientService.Add(client);

            var onlineClients = _clientService.GetAll();
            await Clients.All.SendAsync("ConnectedUsers", onlineClients);

            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            var client = _clientService.FindOnlineClientByConnectionId(Context.ConnectionId);
            _clientService.Delete(client);

            var onlineClients = _clientService.GetAll();
            await Clients.All.SendAsync("ConnectedUsers", onlineClients);

            await base.OnDisconnectedAsync(exception);
        }
    }
}