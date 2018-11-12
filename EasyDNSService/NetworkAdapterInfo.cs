using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Net;
using System.Net.NetworkInformation;
using System.Runtime.Serialization;
using System.Linq;

namespace theDiary.EasyDNS.Windows.Service
{
    [DataContract]
    [KnownType(typeof(WirelessNetworkAdapterInfo))]
    public class NetworkAdapterInfo
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="NetworkAdapterInfo"/> class.
        /// </summary>
        public NetworkAdapterInfo()
            : base()
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NetworkAdapterInfo"/> class, from the specified <paramref name="networkAdapter"/> instance.
        /// </summary>
        /// <param name="deviceName">An string value identifing the name of the device of network adapter.</param>
        /// <exception cref="ArgumentNullException">thrown if the <paramref name="deviceName"/> is <c>Null</c> or <c>Blank</c>.</exception>
        /// <exception cref="ArgumentException">thrown if the specified <paramref name="deviceName"/> is does not belong to a matching network device.</exception>
        public NetworkAdapterInfo(string deviceName)
            : this()
        {
            if (string.IsNullOrWhiteSpace(deviceName))
                throw new ArgumentNullException(nameof(deviceName));

            var networkAdapter = DNSHelper.GetNetworkAdapter(deviceName);
            var networkInterface = DNSHelper.GetNetworkInterface(deviceName);

            if (networkAdapter == null || networkInterface == null)
                throw new ArgumentException($"The device '{deviceName} can not be found.");

            this.InitializeNetworkAdapters(networkAdapter, networkInterface);
        }

        public NetworkAdapterInfo(PhysicalAddress macAddress)
            : this()
        {
            if (macAddress == null)
                throw new ArgumentNullException(nameof(macAddress));

            var networkAdapter = DNSHelper.GetNetworkAdapter(macAddress);
            var networkInterface = DNSHelper.GetNetworkInterface(macAddress);

            if (networkAdapter == null || networkInterface == null)
                throw new ArgumentException($"The device '{macAddress} can not be found.");

            this.InitializeNetworkAdapters(networkAdapter, networkInterface);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NetworkAdapterInfo"/> class, from the specified <paramref name="networkAdapter"/> instance.
        /// </summary>
        /// <param name="networkAdapter">An instance of <see cref="ManagementObject"/> containing the information about the network adapter.</param>
        /// <exception cref="ArgumentNullException">thrown if the <paramref name="networkAdapter"/> is <c>Null</c>.</exception>
        public NetworkAdapterInfo(ManagementObject networkAdapter)
            : this()
        {
            if (networkAdapter == null)
                throw new ArgumentNullException(nameof(networkAdapter));

            this.InitializeNetworkAdapters(networkAdapter, networkInterface);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NetworkAdapterInfo"/> class, from the specified <paramref name="networkAdapter"/> and <paramref name="networkInterface"/> instances.
        /// </summary>
        /// <param name="networkAdapter">An instance of <see cref="ManagementObject"/> containing the information about the network adapter.</param>
        /// <param name="networkInterface">An instance of <see cref="NetworkInterface"/> containing the information about the network interface.</param>
        /// /// <exception cref="ArgumentNullException">thrown if the <paramref name="networkAdapter"/> or <paramref name="networkInterface"/> is <c>Null</c>.</exception>
        public NetworkAdapterInfo(ManagementObject networkAdapter, NetworkInterface networkInterface)
            : this()
        {
            if (networkAdapter == null)
                throw new ArgumentNullException(nameof(networkAdapter));

            if (networkInterface == null)
                throw new ArgumentNullException(nameof(networkInterface));

            this.InitializeNetworkAdapters(networkAdapter, networkInterface);
        }
        #endregion

        #region Private Declarations
        private ManagementObject networkAdapter;
        private NetworkInterface networkInterface;
        #endregion

        #region Public Properties
        [DataMember(Name = "MACAddress")]
        public byte[] MACAddress { get; set; }

        [DataMember(Name = "IPAddress")]
        public IPAddress IPAddress { get; set; }

        [DataMember(Name = "DeviceName")]
        public string DeviceName { get; set; }

        [DataMember(Name = "DNSConfiguration")]
        public DNSConfiguration DNSConfiguration { get; set; }
        #endregion

        #region Public Methods & Functions
        public DNSConfiguration GetDNSConfiguration()
        {
            var servers = this.networkAdapter.GetPropertyValue<string[]>("DnsServerSearchOrder");

            return new DNSConfiguration(servers);
        }

        public uint SetDNSConfiguration(DNSConfiguration configuration)
        {
            ManagementBaseObject args = this.networkAdapter.GetMethodParameters("SetDNSServerSearchOrder");
            args["DNSServerSearchOrder"] = configuration.ToArray();

            ManagementBaseObject invokeResult = this.networkAdapter.InvokeMethod("SetDNSServerSearchOrder", args, null);
            uint returnValue = (uint)invokeResult["ReturnValue"];

            if (returnValue == 0)
                this.DNSConfiguration = configuration;

            return returnValue;
        }
        #endregion

        #region Private Methods & Functions
        private void InitializeNetworkAdapters(ManagementObject networkAdapter, NetworkInterface networkInterface = null)
        {
            this.networkAdapter = networkAdapter;
            this.networkInterface = networkInterface ?? DNSHelper.GetNetworkInterface(this.networkAdapter.GetPropertyValue<string>("Description"));
            this.MACAddress = this.networkAdapter.GetPropertyValue<string>("MACAddress").ToPhysicalAddress().GetAddressBytes();
            this.DNSConfiguration = this.GetDNSConfiguration();
            this.IPAddress = IPAddress.Parse(this.networkAdapter.GetPropertyValue<string[]>("IPAddress").FirstOrDefault());
            this.DeviceName = this.networkInterface.Description; ;
        }
        #endregion

        public NetworkAdapterInfo Clone()
        {
            return new NetworkAdapterInfo()
            {
                DeviceName = this.DeviceName,
                IPAddress = this.IPAddress,
                DNSConfiguration = this.DNSConfiguration
            };
        }

        #region Public Static Methods & Functions
        public static NetworkAdapterInfo[] GetAdapters()
        {
            
            List<NetworkAdapterInfo> returnValue = new List<NetworkAdapterInfo>();
            var adaptors = DNSHelper.GetAvailableNetworkAdapters();
            foreach (var adaptor in adaptors)
            {
                NetworkInterface networkInterface;
                var macAddress = adaptor.GetPropertyValue<string>("MACAddress").ToPhysicalAddress();
                if (DNSHelper.TryGetNetworkInterface(macAddress, out networkInterface))
                    returnValue.Add(NetworkAdapterInfo.Create(adaptor, networkInterface));
            }

            return returnValue.ToArray();
        }

        public static NetworkAdapterInfo Create(ManagementObject networkAdapter, NetworkInterface networkInterface)
        {
            WirelessNetworkInterface wirelessNetworkInterface = Runtime.Instance.WirelessDevices.FirstOrDefault(device => device.MACAddress.Equals(networkInterface.GetPhysicalAddress()));
            if (wirelessNetworkInterface != null)
                return new WirelessNetworkAdapterInfo(networkAdapter, networkInterface, wirelessNetworkInterface);

            return new NetworkAdapterInfo(networkAdapter, networkInterface);
        }
        #endregion
    }
}
