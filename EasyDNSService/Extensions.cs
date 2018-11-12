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


        /// <summary>
        /// Determines if an enumerable sequence is <c>Null</c> or <c>Empty</c>.
        /// </summary>
        /// <typeparam name="T">The <see cref="Type"/>of the sequence to check.</typeparam>
        /// <param name="instance">The enumerable instance to check.</param>
        /// <returns><c>True</c> if the <paramref name="instance"/> is <c>Null</c> or <c>Empty</c>; Otherwise <c>False</c>.</returns>
        public static bool IsNullOrEmpty<T>(this IEnumerable<T> instance)
        {
            return instance == null
                || instance.Count() == 0;
        }
    }
}
