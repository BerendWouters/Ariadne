using System.Net.NetworkInformation;
using System.Threading.Tasks;

namespace Ariadne.Core.Hubs
{
    public interface INotificationClient
    {
        Task IPAddressUpdated(string ipaddress, IPStatus status, long roundtripTime);
    }
}