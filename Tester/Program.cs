using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.ServiceModel;
using System.ServiceModel.Discovery;
using System.Text;
using System.Threading.Tasks;

namespace Tester
{
    class Program
    {
        

        static DNSService.DNSConfiguration g1 = new DNSService.DNSConfiguration() { PrimaryDNS = IPAddress.Parse("8.8.8.8"), SecondaryDNS = IPAddress.Parse("8.8.4.4") };
        static DNSService.DNSConfiguration h1 = new DNSService.DNSConfiguration() { PrimaryDNS = IPAddress.Parse("192.168.0.1") };
        static DNSService.DNSConfiguration h0 = new DNSService.DNSConfiguration();
        static string deviceName;

        static EndpointAddress FindDNSServiceAddress()
        {
            DiscoveryClient discoveryClient = new DiscoveryClient(new UdpDiscoveryEndpoint());
            FindResponse findResponse = discoveryClient.Find(new FindCriteria(typeof(DNSService.IDNSService)));
            if (findResponse.Endpoints.Count > 0)
            {
                return findResponse.Endpoints[0].Address;
            }
            else
            {
                return null;
            }
        }

        private static DNSService.IDNSService CreateProxy(EndpointAddress address)
        {
            var factory = new ChannelFactory<DNSService.IDNSService>(new NetTcpBinding(), address);

            return factory.CreateChannel();
        }
        static void Main(string[] args)
        {
            var address = FindDNSServiceAddress();
            var client = CreateProxy(address);
            //client.Endpoint.Address = FindDNSServiceAddress();
            foreach (var adapter in client.GetNetworkAdapters())
            {
                Console.WriteLine($"{adapter.DeviceName}\t{adapter.IPAddress}");
                if (string.IsNullOrWhiteSpace(deviceName))
                    deviceName = adapter.DeviceName;
            }
            Console.WriteLine("------");
            Console.WriteLine("[1] - Get DNS");
            Console.WriteLine("[2] - Set Google DNS");
            Console.WriteLine("[3] - Set Home DNS - Forced");
            Console.WriteLine("[4] - Set Home DNS - Dynamic");
            Console.WriteLine("[x] - Exit");
            char key;
            while(ProcessKey(out key))
            {
                switch(key)
                { 
                    case '1':
                        var dns = client.GetDNSConfiguration(deviceName);
                        Console.WriteLine($"{dns.Success}\t{dns.Result.PrimaryDNS}\t{dns.Result.SecondaryDNS}");
                        break;
                    case '2':
                        client.ChangeDNS(g1, deviceName);
                        break;
                    case '3':
                        client.ChangeDNS(h1, deviceName);
                        break;
                    case '4':
                        client.ChangeDNS(h0, deviceName);
                        break;
                }
            }
        }

        private static bool ProcessKey(out char key)
        {
            key = Console.ReadKey().KeyChar;
            return (key != 'x' && key != 'X');
        }

    }
}
