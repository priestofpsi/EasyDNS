using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace theDiary.EasyDNS.Windows.Service
{
    [DataContract]
    public class DNSConfiguration
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="DNSConfiguration"/> class.
        /// </summary>
        public DNSConfiguration()
            : base()
        {

        }

        public DNSConfiguration(IEnumerable<string> servers)
            : this()
        {
            if (servers == null || servers.Count() == 0)
                throw new ArgumentException(nameof(servers));

            this.PrimaryDNS = System.Net.IPAddress.Parse(servers.First());
            if (servers.Count() > 1)
                this.SecondaryDNS = System.Net.IPAddress.Parse(servers.Skip(1).First());
        }

        public DNSConfiguration(IEnumerable<System.Net.IPAddress> servers)
            : this()
        {
            if (servers == null || servers.Count() == 0)
                throw new ArgumentException(nameof(servers));

            System.Net.IPAddress[] dnsValues = servers.ToArray();
            this.PrimaryDNS = dnsValues[0];
            if (dnsValues.Length > 1)
                this.SecondaryDNS = dnsValues[1];
        }
        #endregion

        #region Public Properties
        [DataMember(Name = "PrimaryDNS")]
        public System.Net.IPAddress PrimaryDNS { get; set; }

        [DataMember(Name ="SecondaryDNS")]
        public System.Net.IPAddress SecondaryDNS { get; set; }

        public bool IsDHCPAssignedDNSServers
        {
            get
            {
                return this.PrimaryDNS == null;
            }
        }
        #endregion

        #region Public Methods & Functions
        public void UseDHCPAssignedDNSServers()
        {
            this.PrimaryDNS = null;
            this.SecondaryDNS = null;
        }

        public string[] ToArray()
        {
            List<string> returnValue = new List<string>();

            if (this.PrimaryDNS != null)
                returnValue.Add(this.PrimaryDNS.ToString());

            if (this.PrimaryDNS != null && this.SecondaryDNS != null)
                returnValue.Add(this.SecondaryDNS.ToString());

            return returnValue.ToArray();
        }

        public void FromArray(string[] servers)
        {
            if (servers == null)
                throw new ArgumentNullException(nameof(servers));

            this.PrimaryDNS = null;
            this.SecondaryDNS = null;

            if (servers.Count() == 0)
                return;

            this.PrimaryDNS = System.Net.IPAddress.Parse(servers.First());
            if (servers.Count() > 1)
                this.SecondaryDNS = System.Net.IPAddress.Parse(servers.Skip(1).First());
        }

        public override int GetHashCode()
        {
            int hash = 0;
            if (this.PrimaryDNS != null)
                hash = this.PrimaryDNS.GetHashCode();
            if (this.PrimaryDNS != null && this.SecondaryDNS != null)
                hash = hash & this.SecondaryDNS.GetHashCode();

            return (hash == 0) ? base.GetHashCode() : hash;
        }

        public override string ToString()
        {
            return $"[{this.PrimaryDNS}, {this.SecondaryDNS}]";
        }
        #endregion
    }
}
