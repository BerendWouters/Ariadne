using System;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace Ariadne.Core.Hubs
{
    public class NotificationHub : Hub<INotificationClient>
    {
        public override async Task OnConnectedAsync()
        {
            Console.WriteLine(Context.ConnectionId);
            await base.OnConnectedAsync();
        }

        public async Task SendIPAddressStatus(string ipaddress, IPStatus status, long roundtripTime)
        {
            await Clients.All.IPAddressUpdated(ipaddress, status, roundtripTime);
        }
    }
}
