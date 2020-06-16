using System.Net;
using System.Threading.Tasks;

namespace Ariadne.Core
{
    public interface INetworkDeviceDiscoveryService
    {
        Task<PingResponse> Start(IPAddress usableIpAddress);
    }
}