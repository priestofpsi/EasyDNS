using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace theDiary.EasyDNS.Windows
{
    /// <summary>
    /// Contains extension methods used on <see cref="Control"/> implementations.
    /// </summary>
    public static class Extensions
    {        
        public static DNSSetting GetDNSSetting(this DNSService.DNSConfiguration configuration)
        {
            if (configuration == null)
                return null;

            foreach (var dns in Settings.Instance.DNSSettings)
                if (dns.Equals(configuration))
                    return dns;

            return null;
        }

        public static bool IsNullOrEmpty(this Uri uri)
        {
            return uri == null 
                || string.IsNullOrEmpty(uri.ToString());
        }

        /// <summary>
        /// Executes the specified delegate on the thread that owns the control's underlying window handle.
        /// </summary>
        /// <param name="control">A <see cref="System.Windows.Forms.Control"/> instance.</param>
        /// <param name="action">An <see cref="Action"/> method to invoke.</param>
        public static void Invoke(this System.Windows.Forms.Control control, Action action)
        {
            if (action == null)
                throw new ArgumentNullException(nameof(action));

            if (control.InvokeRequired)
            {
                control.Invoke((Delegate)action);
            }
            else
            {
                action();
            }
        }

        /// <summary>
        /// Executes the specified delegate on the thread that owns the control's underlying window handle.
        /// </summary>
        /// <typeparam name="T">The <see cref="Type"/> of the parameter of the method that the <paramref name="action"/> delegate encapsulates.</typeparam>
        /// <param name="control">A <see cref="System.Windows.Forms.Control"/> instance.</param>
        /// <param name="action">An <see cref="Action{T}"/> method to invoke.</param>
        /// <param name="argument">The argument of <see cref="Type"/> <typeparamref name="T"/> to be invoked by the <paramref name="action"/>.</param>
        public static void Invoke<T>(this System.Windows.Forms.Control control, Action<T> action, T argument = default(T))
        {
            if (action == null)
                throw new ArgumentNullException(nameof(action));
            if (control.InvokeRequired)
            {
                control.Invoke((Delegate)action, argument);
            }
            else
            {
                action(argument);
            }
        }

        public static string Truncate(this string text, int length, string ellipsis, bool keepFullWordAtEnd)
        {
            if (string.IsNullOrWhiteSpace(text)) return string.Empty;

            if (text.Length < length) return text;

            text = text.Substring(0, length);

            if (keepFullWordAtEnd)
                text = text.Substring(0, text.LastIndexOf(' '));

            return text + ellipsis;
        }
    }

    
}
