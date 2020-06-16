using Ariadne.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ariadne.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            System.Console.WriteLine("Welcome to Ariadne, a network discovery tool");

            var networkInterfaceDiscoveryService = new NetworkInterfaceDiscoveryService();
            networkInterfaceDiscoveryService.Start();

            System.Console.WriteLine($"Found '{networkInterfaceDiscoveryService.Interfaces.Count}' up network interfaces");


            foreach(var networkInterface in networkInterfaceDiscoveryService.Interfaces)
            {
                System.Console.WriteLine($"Network interface: {networkInterface.Name} | {networkInterface.Type}");
                foreach (var ipv4Addresses in networkInterface.IPv4Addresses)
                {
                    System.Console.WriteLine($"IPv4 Address: {ipv4Addresses}");
                }
                foreach (var networkInterfacePv6Address in networkInterface.IPv6Addresses)
                {
                    System.Console.WriteLine($"IPv6 Address: {networkInterfacePv6Address}");
                }
            }

            System.Console.WriteLine("Hit enter to start discovery of network");
            List<Task> tasks = new List<Task>();
            foreach (var ipv4Address in networkInterfaceDiscoveryService.Interfaces.SelectMany(x => x.IPv4Addresses))
            {
                //tasks.Add(new NetworkDiscoveryService().Start(ipv4Address.Address));
            }

            Task.WaitAll(tasks.ToArray());
        }
    }
}
