using System;
using System.Net;
using System.Net.NetworkInformation;
using System.Threading.Tasks;

namespace Ariadne.Core
{
    public class NetworkDeviceDiscoveryService : INetworkDeviceDiscoveryService
    {
        public async Task<PingResponse> Start(IPAddress usableIpAddress)
        {
            var ping = new Ping();
            var reply = await ping.SendPingAsync(usableIpAddress);
            return new PingResponse()
            {
                IPAddress = reply.Address,
                Status = reply.Status,
                RoundtripTime = reply.RoundtripTime
            };
        }
    }
}