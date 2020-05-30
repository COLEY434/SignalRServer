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
        public async Task skfjks()
        {
            Clients.Users()
        }

    }
}
