using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Ariadne.Core.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace Ariadne.Core
{
    public class NetworkDiscoveryService : INetworkDiscoveryService
    {
        private readonly INetworkDeviceDiscoveryService _networkDeviceDiscoveryService;
        private readonly IHubContext<NotificationHub, INotificationClient> _hubContext;

        public NetworkDiscoveryService(INetworkDeviceDiscoveryService networkDeviceDiscoveryService, IHubContext<NotificationHub, INotificationClient> hubContext)
        {
            _networkDeviceDiscoveryService = networkDeviceDiscoveryService;
            _hubContext = hubContext;
        }
        public async Task Start(IReadOnlyList<IPAddressStatus> ipAddresses)
        {
            var responses = new List<PingResponse>();
            foreach (var ipAddress in ipAddresses)
            {
                Console.WriteLine($"Starting ping for '{ipAddress.IPAddress}'");
                var task = _networkDeviceDiscoveryService.Start(ipAddress.IPAddress);
                _ = task.ContinueWith(res => UpdateResponse(ipAddress, res.Result));
            }
        }

        private void UpdateResponse(IPAddressStatus ipAddressStatus, PingResponse result)
        {
            _hubContext.Clients.All.IPAddressUpdated(ipAddressStatus.IPAddress.ToString(), result.Status, result.RoundtripTime);
            //Alert(result);
        }

        private void Alert(PingResponse result)
        {
            Console.WriteLine($"Received response for {result.IPAddress.ToString()}: {result.Status}");
        }
    }
}