using System;
using System.Management;
using System.Net;
using System.Net.NetworkInformation;

namespace Tester
{

    public class NetworkAdapterInfo2
    {
        private ManagementObject deviceInfo; //Get-WmiObject -Query "select * from Win32_NetworkAdapter where PhysicalAdapter='true' and NetEnabled='true'" | Format-List -Property *
        private ManagementObject deviceConfigurationInfo; //Get-WmiObject -Query "select * from Win32_NetworkAdapterConfiguration where IPEnabled='true'" | Format-List -Property *

        //deviceInfo["MACAddress
        public PhysicalAddress MACAddress { get; set; }

        //deviceInfo["Name"]
        public string DeviceName { get; set; }

        //deviceInfo["Description"]
        public string DeviceDescription { get; set; }

        //deviceInfo["Manufacturer
        public string DeviceManufacturer { get; set; }

        //deviceInfo["NetConnectionID
        public string NetworkName { get; set; }

        //deviceInfo["NetConnectionStatus"]
        public NetConnectionStatus Status { get; }

        //deviceInfo["DeviceID
        public int DeviceID { get; set; }

        //deviceInfo["GUID"]
        public Guid CurrentConfiguration { get; set; }

        //deviceInfo["Speed"]
        public int Speed { get; set; }

        //deviceConfigurationInfo["IPAddress"]
        public IPAddress IPAddress { get; set; }

        //deviceConfigurationInfo["DHCPServer"]
        public IPAddress DHCPServer { get; set; }

        //deviceConfigurationInfo["DefaultIPGateway"]
        public IPAddress DefaultIPGateway { get; set; }

        //deviceConfigurationInfo["IPSubnet"]
        public IPAddress[] IPSubnet { get; set; }

        //deviceConfigurationInfo["DHCPEnabled"]
        public bool DHCPEnabled { get; set; }

        //deviceConfigurationInfo["DNSServerSearchOrder"] : {23.21.43.50, 54.229.171.234}
        public IPAddress[] DNSServers { get; set; }

        //WlanClient
    }

    public enum NetConnectionStatus
    {
        Disconnected = 0,
        Connecting = 1,
        Connected = 2,
        Disconnecting = 3,
        HardwareNotPresent = 4,
        HardwareDisabled = 5,
        HardwareMalfunction = 6,
        MediaDisconnected = 7,
        Authenticating = 8,
        AuthenticationSucceeded = 9,
        AuthenticationFailed = 10,
        InvalidAddress = 11,
        CredentialsRequired = 12,
        Other = 13
    }
}
