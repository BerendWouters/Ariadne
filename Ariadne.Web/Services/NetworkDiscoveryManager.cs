using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Ariadne.Core;

namespace Ariadne.Web.Services
{
    public class NetworkDiscoveryManager
    {
        private readonly INetworkDiscoveryService _networkDiscoveryService;
        public List<IPAddressStatus> IPAddresses { get;private set; }

        public NetworkDiscoveryManager(INetworkDiscoveryService networkDiscoveryService)
        {
            _networkDiscoveryService = networkDiscoveryService;
            IPAddresses = new List<IPAddressStatus>();
        }

        public async Task Discover(DiscoveredIPAddress ipAddress)
        {

            var parsedNetwork = IPNetwork.Parse(ipAddress.Address, ipAddress.Mask);
            IPAddresses = parsedNetwork.ListIPAddress(FilterEnum.Usable).Select(x => new IPAddressStatus(){IPAddress = x}).ToList();

            await _networkDiscoveryService.Start(IPAddresses);
        }
    }
}
