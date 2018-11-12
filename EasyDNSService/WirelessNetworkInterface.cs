using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace theDiary.EasyDNS.Windows.Service
{
    public class WirelessNetworkInterface
    {
        #region Constructors
        public WirelessNetworkInterface(System.Collections.Generic.Dictionary<string, string> values)
        {
            this.values = values;
            if (this.values.ContainsKey("Physical address"))
                this.macAddress = PhysicalAddress.Parse(this.values["Physical address"].ToUpper().Replace(':', '-'));
        }
        #endregion

        #region Private Declarations
        private System.Collections.Generic.Dictionary<string, string> values;
        private PhysicalAddress macAddress = null;
        #endregion

        #region Public Properties
        public PhysicalAddress MACAddress
        {
            get
            {
                return this.macAddress;
            }
        }

        public string SSID
        {
            get
            {
                if (this.values.ContainsKey("SSID"))
                    return this.values["SSID"];

                return null;
            }
        }

        public string RadioType
        {
            get
            {
                if (this.values.ContainsKey("Radio type"))
                    return this.values["Radio type"];

                return null;
            }
        }

        public string Authentication
        {
            get
            {
                if (this.values.ContainsKey("Authentication"))
                    return this.values["Authentication"];

                return null;
            }
        }

        public int Channel
        {
            get
            {
                int channel;
                if (this.values.ContainsKey("Channel")
                    && int.TryParse(this.values["Channel"], out channel))
                    return channel;

                return -1;
            }
        }

        public int Signal
        {
            get
            {
                int signal;
                if (this.values.ContainsKey("Signal")
                    && int.TryParse(this.values["Signal"].Replace("%", ""), out signal))
                    return signal;

                return 0;
            }
        }

        public int RxSpeed
        {
            get
            {
                int speed;
                if (this.values.ContainsKey("Receive rate (Mbps)")
                    && int.TryParse(this.values["Receive rate (Mbps)"], out speed))
                    return speed;

                return 0;
            }
        }

        public int TxSpeed
        {
            get
            {
                int speed;
                if (this.values.ContainsKey("Transmit rate (Mbps)")
                    && int.TryParse(this.values["Transmit rate (Mbps)"], out speed))
                    return speed;

                return -1;
            }
        }
        #endregion
    }
}
