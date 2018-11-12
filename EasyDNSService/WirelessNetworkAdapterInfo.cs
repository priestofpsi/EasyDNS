using System;
using System.Management;
using System.Net.NetworkInformation;
using System.Runtime.Serialization;
using System.Linq;

namespace theDiary.EasyDNS.Windows.Service
{
    [DataContract]
    public class WirelessNetworkAdapterInfo
        : NetworkAdapterInfo
    {
        #region Constructors
        public WirelessNetworkAdapterInfo()
            : base()
        {

        }

        public WirelessNetworkAdapterInfo(string deviceName)
            : base(deviceName)
        {

        }

        public WirelessNetworkAdapterInfo(PhysicalAddress macAddress)
            : base(macAddress)
        {
            this.Populate();
        }

        public WirelessNetworkAdapterInfo(ManagementObject networkAdapter)
            : base(networkAdapter)
        {            
            this.Populate();
        }

        public WirelessNetworkAdapterInfo(ManagementObject networkAdapter, NetworkInterface networkInterface)
            : base(networkAdapter, networkInterface)
        {
            this.Populate();
        }

        public WirelessNetworkAdapterInfo(ManagementObject networkAdapter, NetworkInterface networkInterface, WirelessNetworkInterface wirelessNetworkInterface)
            : base(networkAdapter, networkInterface)
        {
            if (wirelessNetworkInterface == null)
                throw new ArgumentNullException(nameof(wirelessNetworkInterface));

            this.Populate(wirelessNetworkInterface);
        }
        #endregion

        private void Populate()
        {
            WirelessNetworkInterface wirelessNetworkInterface = Runtime.Instance.WirelessDevices.FirstOrDefault(device => device.MACAddress.Equals(this.MACAddress));
            this.Populate(wirelessNetworkInterface);
        }

        private void Populate(WirelessNetworkInterface wirelessNetworkInterface)
        {
            this.SSID = wirelessNetworkInterface.SSID;
            this.Channel = wirelessNetworkInterface.Channel;
            this.Signal = wirelessNetworkInterface.Signal;
            this.RadioType = wirelessNetworkInterface.RadioType;
            this.Authentication = wirelessNetworkInterface.Authentication;
            this.ReceiveSpeed = wirelessNetworkInterface.RxSpeed;
            this.TransmitSpeed = wirelessNetworkInterface.TxSpeed;
        }
        
        [DataMember(Name = "SSID")]
        public string SSID { get; set; }

        [DataMember(Name = "Channel")]
        public int Channel { get; set; }

        [DataMember(Name = "Signal")]
        public int Signal { get; set; }

        [DataMember(Name = "RadioType")]
        public string RadioType { get; set; }

        [DataMember(Name = "Authentication")]
        public string Authentication { get; set; }

        [DataMember(Name = "ReceiveSpeed")]
        public int ReceiveSpeed { get; set; }

        [DataMember(Name = "TransmitSpeed")]
        public int TransmitSpeed { get; set; }
    }
}
