using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;

namespace Ariadne.Core
{
    public class NetworkInterfaceDiscoveryService
    {

        public NetworkInterfaceDiscoveryService()
        {
            Interfaces = new List<DiscoveredNetworkInterface>();
        }
        public void Start()
        {
            var upInterfaces =
                NetworkInterface.GetAllNetworkInterfaces()
                    .Where(x => 
                        x.OperationalStatus == OperationalStatus.Up &&
                        (x.NetworkInterfaceType == NetworkInterfaceType.Ethernet ||
                        x.NetworkInterfaceType == NetworkInterfaceType.Wireless80211))
                    .ToList();
            Interfaces = upInterfaces.Select(x => new DiscoveredNetworkInterface()
            {
                Name = x.Name,
                Type = x.NetworkInterfaceType,
                IPv4Addresses = 
                    x.GetIPProperties()
                        .UnicastAddresses
                        .Where(uc => uc.Address.AddressFamily == AddressFamily.InterNetwork)
                        .Select(ua => new DiscoveredIPAddress
                        {
                            Address = ua.Address,
                            Mask = ua.IPv4Mask
                        })
                        .ToList(),
                IPv6Addresses =
                x.GetIPProperties()
                .UnicastAddresses
                .Where(uc => uc.Address.AddressFamily == AddressFamily.InterNetworkV6)
                .Select(ua => new DiscoveredIPAddress
                {
                    Address = ua.Address,
                    Mask = ua.IPv4Mask
                })
                .ToList()
            }).ToList();
        }

        public List<DiscoveredNetworkInterface> Interfaces { get; set; }


        public class DiscoveredNetworkInterface
        {
            public string Name { get; set; }
            public NetworkInterfaceType Type { get; set; }

            public DiscoveredNetworkInterface()
            {
                IPv4Addresses = new List<DiscoveredIPAddress>();
                IPv6Addresses = new List<DiscoveredIPAddress>();
            }
            public List<DiscoveredIPAddress> IPv4Addresses { get; set; }
            public List<DiscoveredIPAddress> IPv6Addresses { get; set; }
        }
    }

    public class DiscoveredIPAddress
    {
        public IPAddress Address { get; set; }
        public IPAddress Mask { get; set; }
    }
}