using theDiary.EasyDNS.Windows.DNSService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace theDiary.EasyDNS.Windows
{
    public class NetworkDevice
    {
        #region Public Constructors
        public NetworkDevice(DNSService.NetworkAdapterInfo adapterInfo)
        {
            this.adapterInfo = adapterInfo;
        }
        #endregion

        #region Private Declarations
        private DNSService.NetworkAdapterInfo adapterInfo;
        #endregion

        #region Public Properties
        public string DeviceName
        {
            get
            {
                return this.adapterInfo.DeviceName;
            }
        }

        public PhysicalAddress MACAddress { get
            {
                return new PhysicalAddress(this.adapterInfo.MACAddress);
            }
        }

        public string ShortDeviceName

        {
            get
            {
                return this.DeviceName.Truncate(30, "...", true);
            }
        }

        public DNSConfiguration DNSConfiguration
        {
            get
            {
                return this.adapterInfo.DNSConfiguration;
            }
        }

        public IPAddress IPAddress
        {
            get
            {
                return this.adapterInfo.IPAddress;
            }
        }

        public void UpdateDNSConfiguration(DNSConfiguration configuration)
        {
            if (configuration == null)
                return;

            this.adapterInfo.DNSConfiguration = configuration;
        }
        #endregion

        #region Public Methods & Functions
        public override string ToString()
        {
            return string.Format("{0} ({1})", this.ShortDeviceName, this.IPAddress);
        }
        #endregion

    }
}
