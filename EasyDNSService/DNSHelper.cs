using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Net.NetworkInformation;
using System.Text;

namespace theDiary.EasyDNS.Windows.Service
{
    public static class DNSHelper
    {
        #region Private Declarations
        private static string[] emptyDHCP = new string[] { "", "255.255.255.255" };
        
        #endregion

        #region Private Properties
        private static ManagementObjectCollection AdaptersCollection
        {
            get
            {
                ManagementObjectSearcher adapters = new ManagementObjectSearcher
                {
                    Query = new ObjectQuery(@"SELECT * FROM Win32_NetworkAdapterConfiguration WHERE IPEnabled = TRUE")
                };

                return adapters.Get();
            }
        }

        

        #endregion

        #region Internal Static Methods
        #region Network Interface Methods & Functions
        internal static NetworkInterface GetNetworkInterface(string deviceName)
        {
            NetworkInterface networkInterface = NetworkInterface.GetAllNetworkInterfaces().Where(@interface =>
                @interface.OperationalStatus == OperationalStatus.Up
                    && @interface.Description.Equals(deviceName)).FirstOrDefault();

            return networkInterface;
        }

        internal static NetworkInterface GetNetworkInterface(PhysicalAddress macAddress)
        {
            NetworkInterface networkInterface = NetworkInterface.GetAllNetworkInterfaces().Where(@interface =>
                @interface.OperationalStatus == OperationalStatus.Up
                    && @interface.GetPhysicalAddress().Equals(macAddress)).FirstOrDefault();

            return networkInterface;
        }

        internal static IEnumerable<NetworkInterface> GetNetworkInterfaces()
        {
            foreach (var a in System.Net.NetworkInformation.NetworkInterface.GetAllNetworkInterfaces().Where(a => a.OperationalStatus == System.Net.NetworkInformation.OperationalStatus.Up))
                yield return a;
        }

        internal static IEnumerable<NetworkInterface> GetNetworkInterfaces(IEnumerable<string> deviceNames)
        {
            foreach (var a in System.Net.NetworkInformation.NetworkInterface.GetAllNetworkInterfaces().Where(a => a.OperationalStatus == System.Net.NetworkInformation.OperationalStatus.Up 
            && deviceNames.Contains(a.Description)))
                yield return a;
        }

        internal static IEnumerable<NetworkInterface> GetNetworkInterfaces(IEnumerable<PhysicalAddress> macAddresses)
        {
            foreach (var a in System.Net.NetworkInformation.NetworkInterface.GetAllNetworkInterfaces().Where(a => a.OperationalStatus == System.Net.NetworkInformation.OperationalStatus.Up 
            && macAddresses.Contains(a.GetPhysicalAddress())))
                yield return a;
        }

        internal static bool TryGetNetworkInterface(string deviceName, out NetworkInterface networkInterface)
        {
            networkInterface = NetworkInterface.GetAllNetworkInterfaces().Where(@interface =>
                @interface.OperationalStatus == OperationalStatus.Up
                    && @interface.Description.Equals(deviceName)).FirstOrDefault();

            return networkInterface != null;
        }

        internal static bool TryGetNetworkInterface(PhysicalAddress macAddress, out NetworkInterface networkInterface)
        {
            networkInterface = NetworkInterface.GetAllNetworkInterfaces().Where(@interface =>
                @interface.OperationalStatus == OperationalStatus.Up
                    && @interface.GetPhysicalAddress().Equals(macAddress)).FirstOrDefault();

            return networkInterface != null;
        }
        #endregion

        #region Network Adapters Methods & Functions
        internal static IEnumerable<ManagementObject> GetAvailableNetworkAdapters()
        {
            foreach (ManagementObject adapter in DNSHelper.AdaptersCollection)
                if (!adapter.GetPropertyValue<string>("DHCPServer").ContainsAny(emptyDHCP))
                    yield return adapter;
        }

        internal static IEnumerable<string> GetAvailableNetworkAdapterNames()
        {
            foreach (ManagementObject adapter in DNSHelper.AdaptersCollection)
            {
                if (!adapter.GetPropertyValue<string>("DHCPServer").Trim().ContainsAny(emptyDHCP))
                    yield return adapter.GetPropertyValue("Description") as string;
            }
        }

        internal static ManagementObject GetNetworkAdapter(string deviceName)
        {
            foreach (ManagementObject adapter in DNSHelper.AdaptersCollection)
                if (adapter.GetPropertyValue<string>("Description").Equals(deviceName)
                    && !adapter.GetPropertyValue<string>("DHCPServer").Trim().ContainsAny(emptyDHCP))
                    return adapter;

            return null;
        }

        internal static ManagementObject GetNetworkAdapter(PhysicalAddress macAddress)
        {
            foreach (ManagementObject adapter in DNSHelper.AdaptersCollection)
                if (adapter.GetPropertyValue<string>("MACAddress").ToPhysicalAddress().Equals(macAddress)
                    && !adapter.GetPropertyValue<string>("DHCPServer").Trim().ContainsAny(emptyDHCP))
                    return adapter;

            return null;
        }

        internal static bool TryGetNetworkAdapter(string deviceName, out ManagementObject networkAdapter)
        {
            networkAdapter = DNSHelper.GetNetworkAdapter(deviceName);
            return networkAdapter != null;
        }

        internal static bool TryGetNetworkAdapter(PhysicalAddress macAddress, out ManagementObject networkAdapter)
        {
            networkAdapter = DNSHelper.GetNetworkAdapter(macAddress);
            return networkAdapter != null;
        }
        #endregion


        internal static IEnumerable<WirelessNetworkInterface> GetWirelessDevices()
        {
            string noInterfaces = "There is no wireless interface on the system.";
            
            System.Diagnostics.ProcessStartInfo psi = new System.Diagnostics.ProcessStartInfo("netsh.exe", "wlan show interfaces");
            psi.RedirectStandardOutput = true;
            psi.UseShellExecute = false;
            System.Diagnostics.Process process = new System.Diagnostics.Process();
            process.StartInfo = psi;
            process.Start();

            StringBuilder sb = new StringBuilder(process.StandardOutput.ReadToEnd());
            bool newDetails = true;
            System.Collections.Generic.List<WirelessNetworkInterface> devices = new System.Collections.Generic.List<WirelessNetworkInterface>();
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
                            devices.Add(new WirelessNetworkInterface(details));
                        newDetails = true;
                        details = null;
                    }
                    line = reader.ReadLine();
                }
            }

            process.WaitForExit();

            return devices;
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
        


        #endregion

    }
}
