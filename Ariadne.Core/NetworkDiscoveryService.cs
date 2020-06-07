using System.Net;
using System.Threading.Tasks;

namespace Ariadne.Core
{
    public class NetworkDiscoveryService
    {
        public async Task Start(DiscoveredIPAddress ipAddress)
        {
            var parsedNetwork = IPNetwork.Parse(ipAddress.Address, ipAddress.Mask);
            var networkDeviceDiscoveryServiceService = new NetworkDeviceDiscoveryService();
            foreach (var usableIPAddress in parsedNetwork.ListIPAddress(FilterEnum.Usable))
            {
                await networkDeviceDiscoveryServiceService.Start(usableIPAddress);
            }
        }
    }
}