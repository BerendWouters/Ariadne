using System;
using System.Net;
using System.Net.NetworkInformation;
using System.Threading.Tasks;

namespace Ariadne.Core
{
    public class NetworkDeviceDiscoveryService
    {
        public async Task Start(IPAddress usableIpAddress)
        {
            Console.WriteLine($"Starting ping for '{usableIpAddress}'");
            var ping = new Ping();
            var reply = await ping.SendPingAsync(usableIpAddress);
            Console.WriteLine($"Ping for '{reply.Address}' returned: '{reply.Status}'");
        }
    }
}