using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace theDiary.EasyDNS.Windows
{
    [Serializable]
    public partial class Settings
        : ISerializable
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Settings"/> class.
        /// </summary>
        private Settings()
        {
            this.DNSSettings = new DNSSettingCollection();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Settings"/> class from the specified <paramref name="info"/> and <paramref name="context"/>.
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        private Settings(SerializationInfo info, StreamingContext context)
        {
            this.EndPointAddressUri = (Uri) info.GetValue(nameof(EndPointAddressUri), typeof(Uri));
            this.DNSSettings = (DNSSettingCollection)info.GetValue(nameof(DNSSettings), typeof(DNSSettingCollection));
            this.FormLocation = (Point)info.GetValue(nameof(FormLocation), typeof(Point));
            this.FormSize = (Size)info.GetValue(nameof(FormSize), typeof(Size));
        }
        #endregion

        #region Public Properties
        /// <summary>
        /// Gets or sets the <see cref="Uri"/> for the EndPoint for the <see cref="DNSService.IDNSService"/>.
        /// </summary>
        public Uri EndPointAddressUri { get; set; }

        public DNSSettingCollection DNSSettings { get; set; }

        /// <summary>
        /// Gets or sets the Location of the application form.
        /// </summary>
        public Point FormLocation { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="Size"/> of the application form.
        /// </summary>
        public Size FormSize { get; set; }
        #endregion
        
        #region Public Methods & Functions
        void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue(nameof(this.EndPointAddressUri), this.EndPointAddressUri);
            info.AddValue(nameof(this.DNSSettings), this.DNSSettings);
            info.AddValue(nameof(this.FormLocation), this.FormLocation);
            info.AddValue(nameof(this.FormSize), this.FormSize);
        }
        #endregion
    }
}
