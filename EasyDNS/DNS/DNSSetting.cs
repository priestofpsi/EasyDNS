using theDiary.EasyDNS.Windows.DNSService;
using System;
using System.Net;
using System.Runtime.Serialization;

namespace theDiary.EasyDNS.Windows
{
    [Serializable]
    public class DNSSetting
        : IEquatable<DNSSetting>,
        IEquatable<DNSService.DNSConfiguration>,
        ISerializable

    {
        #region Constructors
        private DNSSetting(SerializationInfo info, StreamingContext context)
        {
            this.Name = info.GetString("Name");
            this.PrimaryDNS = (IPAddress)info.GetValue("PrimaryDNS", typeof(IPAddress));
            this.SecondaryDNS = (IPAddress)info.GetValue("SecondaryDNS", typeof(IPAddress));
        }

        public DNSSetting()
            : this(string.Empty)
        {

        }

        public DNSSetting(string name)
            : base()
        {
            this.name = name;
        }

        public DNSSetting(string name, string primaryDNS, string secondaryDNS = null)
            : this(name)
        {
            this.PrimaryDNS = IPAddress.Parse(primaryDNS);
            if (!string.IsNullOrWhiteSpace(secondaryDNS))
                this.SecondaryDNS = IPAddress.Parse(secondaryDNS);
        }
        #endregion

        #region Private Declarations
        private string name;
        #endregion

        #region Public Properties
        public string Name
        {
            get
            {
                return this.name;
            }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentNullException(nameof(name));
                if (!value.Equals(name))
                    this.name = value;
            }
        }

        public bool IsEmpty
        {
            get
            {
                return string.IsNullOrWhiteSpace(this.Name);
            }
        }

        public IPAddress PrimaryDNS { get; set; }

        public IPAddress SecondaryDNS { get; set; }
        internal string PrimaryDNSText
        {
            get
            {
                if (this.PrimaryDNS == null)
                    return string.Empty;
                return this.PrimaryDNS.ToString();
            }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    this.PrimaryDNS = null;
                this.PrimaryDNS = IPAddress.Parse(value);
            }
        }

        internal string SecondaryDNSText
        {
            get
            {
                if (this.SecondaryDNS == null)
                    return string.Empty;
                return this.SecondaryDNS.ToString();
            }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    this.SecondaryDNS = null;
                this.SecondaryDNS = IPAddress.Parse(value);
            }
        }
        public bool Equals(DNSConfiguration other)
        {
            if (other == null)
                return false;

            bool primary, secondary;
            primary = (this.PrimaryDNS == null && other.PrimaryDNS == null) || (this.PrimaryDNS != null && this.PrimaryDNS.Equals(other.PrimaryDNS));
            secondary = (this.SecondaryDNS == null && other.SecondaryDNS == null) || (this.SecondaryDNS != null && this.SecondaryDNS.Equals(other.SecondaryDNS));
            return primary && secondary;
        }

        public bool Equals(DNSSetting other)
        {
            if (other == null)
                return false;

            return this.Name.Equals(other.Name);
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Name", this.Name);
            info.AddValue("PrimaryDNS", this.PrimaryDNS);
            info.AddValue("SecondaryDNS", this.SecondaryDNS);
        }

        public static implicit operator DNSConfiguration(DNSSetting value)
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value));

            return new DNSConfiguration() { PrimaryDNS = value.PrimaryDNS, SecondaryDNS = value.SecondaryDNS };
        }

        public DNSConfiguration CreateConfiguration()
        {
            return new DNSConfiguration() { PrimaryDNS = this.PrimaryDNS, SecondaryDNS = this.SecondaryDNS };
        }
        #endregion
    }



}
