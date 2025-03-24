using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace CarShop.API.Hubs
{
    public class ExampleHub: Hub
    {
        public async Task BroadcastMessageToAllClient(string message)
        {
            await Clients.All.SendAsync("ReceiveMessageAllForClient", message);
        }
    }
}