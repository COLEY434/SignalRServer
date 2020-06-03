using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalServer.Hubs
{
    [Authorize]
    public class ChatHub : Hub
    {
        public string getConnectionId()
        {
            
            return Context.ConnectionId;
        }

        public override async Task OnConnectedAsync()
        {
            await Clients.Caller.SendAsync("retrieveToken", Context.ConnectionId);
            await base.OnConnectedAsync();
        }

      

    }
}
