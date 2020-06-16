using System.Collections.Generic;
using System.Net;
using System.Net.NetworkInformation;
using System.Threading.Tasks;

namespace Ariadne.Core
{
    public interface INetworkDiscoveryService
    {
        Task Start(IReadOnlyList<IPAddressStatus> ipAddress);

    }

    public class IPAddressStatus
    {
        public IPAddressStatus()
        {
            Response = IPStatus.Unknown;
        }
        public IPAddress IPAddress { get; set; }
        public IPStatus Response { get; set; }
        public long RoundtripTime { get; set; }
    }
}