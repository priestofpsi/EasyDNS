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
    /// <summary>
    /// Wrapper class for the <see cref="DNSService.NetworkAdapterInfo"/>.
    /// </summary>
    public class NetworkDevice
    {
        #region Public Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="NetworkDevice"/> class from the specified <paramref name="adapterInfo"/>.
        /// </summary>
        /// <param name="adapterInfo">An instance of <see cref="DNSService.NetworkAdapterInfo"/> used to initialize the <see cref="NetworkDevice"/>.</param>
        public NetworkDevice(DNSService.NetworkAdapterInfo adapterInfo)
        {
            this.adapterInfo = adapterInfo;
        }
        #endregion

        #region Private Declarations
        private DNSService.NetworkAdapterInfo adapterInfo;
        #endregion

        #region Public Properties
        /// <summary>
        /// Gets the name of the device.
        /// </summary>
        public string DeviceName
        {
            get
            {
                return this.adapterInfo.DeviceName;
            }
        }

        /// <summary>
        /// Gets the <see cref="PhysicalAddress"/> of the device.
        /// </summary>
        public PhysicalAddress MACAddress { get
            {
                return new PhysicalAddress(this.adapterInfo.MACAddress);
            }
        }

        /// <summary>
        /// Gets a short name of the Device
        /// </summary>
        public string ShortDeviceName

        {
            get
            {
                return this.DeviceName.Truncate(30, "...", true);
            }
        }

        /// <summary>
        /// Gets the current <see cref="DNSConfiguration"/> of the device.
        /// </summary>
        public DNSConfiguration DNSConfiguration
        {
            get
            {
                return this.adapterInfo.DNSConfiguration;
            }
        }

        /// <summary>
        /// Gets the <see cref="IPAddress"/> of the device.
        /// </summary>
        public IPAddress IPAddress
        {
            get
            {
                return this.adapterInfo.IPAddress;
            }
        }
        #endregion

        #region Public Methods & Functions

        public void UpdateDNSConfiguration(DNSConfiguration configuration)
        {
            if (configuration == null)
                return;

            this.adapterInfo.DNSConfiguration = configuration;
        }
        
        public override string ToString()
        {
            return string.Format("{0} ({1})", this.ShortDeviceName, this.IPAddress);
        }
        #endregion

    }
}
