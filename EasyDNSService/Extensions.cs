using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace theDiary.EasyDNS.Windows.Service
{
    public static class Extensions
    {
        internal static T GetPropertyValue<T>(this ManagementObject instance, string propertyName)
        {
            return (T)instance.GetPropertyValue(propertyName);
        }

        internal static bool ContainsAny(this string value, IEnumerable<string> values)
        {
            if (value == null)
                value = string.Empty;
            foreach (var val in values)
                if (val.Equals(value, StringComparison.OrdinalIgnoreCase))
                    return true;

            return false;
        }

        public static PhysicalAddress ToPhysicalAddress(this string macAddress)
        {
            return PhysicalAddress.Parse(macAddress.Replace(':', '-'));
        }
    }
}
