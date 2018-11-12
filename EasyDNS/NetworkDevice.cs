using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using theDiary.EasyDNS.Windows.DNSService;
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
            var a = GetWirelessDevice(this.MACAddress);
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
        public PhysicalAddress MACAddress
        {
            get
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

        private string ssid;
        public string SSID
        {
            get
            {
                if (string.IsNullOrWhiteSpace(this.ssid))
                    this.IsWIFI = TryGetSSID(this.MACAddress, out this.ssid);

                return this.ssid;
            }
        }

        public bool IsWIFI { get; set; }
        public override string ToString()
        {
            return string.Format("{0} ({1})", this.ShortDeviceName, this.IPAddress);
        }
        #endregion

        internal static bool TryGetSSID(PhysicalAddress macAddress, out string ssid)
        {
            System.Diagnostics.ProcessStartInfo psi = new System.Diagnostics.ProcessStartInfo("netsh.exe", "wlan show interfaces");
            psi.RedirectStandardOutput = true;
            psi.UseShellExecute = false;
            System.Diagnostics.Process process = new System.Diagnostics.Process();
            process.StartInfo = psi;
            process.Start();

            string output = process.StandardOutput.ReadToEnd();
            ssid = output.Substring(output.IndexOf("SSID"));
            ssid = ssid.Substring(ssid.IndexOf(":") + 2, ssid.IndexOf("\n")).Trim();
            process.WaitForExit();

            return string.IsNullOrWhiteSpace(ssid);
        }

        internal static WirelessDevice GetWirelessDevice(PhysicalAddress macAddress)
        {
            string noInterfaces = "There is no wireless interface on the system.";
            string ssid;
            System.Diagnostics.ProcessStartInfo psi = new System.Diagnostics.ProcessStartInfo("netsh.exe", "wlan show interfaces");
            psi.RedirectStandardOutput = true;
            psi.UseShellExecute = false;
            System.Diagnostics.Process process = new System.Diagnostics.Process();
            process.StartInfo = psi;
            process.Start();

            WirelessDevice returnValue = null;

            StringBuilder sb = new StringBuilder(process.StandardOutput.ReadToEnd());
            bool newDetails = true;
            System.Collections.Generic.List<WirelessDevice> devices = new System.Collections.Generic.List<WirelessDevice>();
            using (System.IO.StringReader reader = new System.IO.StringReader(sb.ToString()))
            {
                System.Collections.Generic.Dictionary<string, string> details = null;
                var line = reader.ReadLine();
                if (line != null && line.Trim().Equals(noInterfaces, System.StringComparison.OrdinalIgnoreCase))
                    return null;

                while (line != null)
                {
                    if (!string.IsNullOrWhiteSpace(line)
                        && !line.Trim().EndsWith(":\r\n")
                        && !IsNumberOfWirelessDevices(line))
                    {
                        if (newDetails && details == null)
                        {
                            newDetails = false;
                            details = new System.Collections.Generic.Dictionary<string, string>();
                        }
                        var ab = line.Trim().Split(new string[] { " : " }, System.StringSplitOptions.None);
                        details.Add(ab[0].Trim(), ab[1].Trim());
                    }
                    else if (!newDetails)
                    {
                        if (details.ContainsKey("Physical address"))
                            devices.Add(new WirelessDevice(details));
                        newDetails = true;
                        details = null;
                    }
                    line = reader.ReadLine();
                }
            }

            process.WaitForExit();

            returnValue = devices.FirstOrDefault(dev => dev.MACAddress.Equals(macAddress));
            return returnValue;
        }

        private static bool IsNumberOfWirelessDevices(string value)
        {
            string txt = "There is 1 interface on the system:";

            string re1 = "(There)( )(is)( )(\\d+)( )(interface)( )(on)( )(the)( )(system)(:)";   // Word 6
            //string re2 = "(There\\s+is )(\\d+)( interface on the)( )(system)(:)";   // Word 6

            System.Text.RegularExpressions.Regex r = new System.Text.RegularExpressions.Regex(re1, System.Text.RegularExpressions.RegexOptions.IgnoreCase | System.Text.RegularExpressions.RegexOptions.Singleline);
            System.Text.RegularExpressions.Match m = r.Match(value);
            return m.Success;
        }

        internal class WirelessDevice
        {
            public WirelessDevice(System.Collections.Generic.Dictionary<string, string> values)
            {
                this.values = values;
                if (this.values.ContainsKey("Physical address"))
                    this.macAddress = PhysicalAddress.Parse(this.values["Physical address"].ToUpper().Replace(':', '-'));
            }

            private System.Collections.Generic.Dictionary<string, string> values;
            private PhysicalAddress macAddress = null;
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
        }
    }
}
