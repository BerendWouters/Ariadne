using System.Net;
using System.Net.NetworkInformation;

namespace Ariadne.Core
{
    public class PingResponse
    {
        public IPAddress IPAddress { get; set; }
        public IPStatus Status { get; set; }
        public long RoundtripTime { get; set; }
    }
}