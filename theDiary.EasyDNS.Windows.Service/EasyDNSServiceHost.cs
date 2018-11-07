using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.ServiceModel.Discovery;
using System.Text;
using System.Threading.Tasks;

namespace theDiary.EasyDNS.Windows.Service
{
    public class EasyDNSServiceHost
        : ServiceHost
    {
        private const string EndPointAddress = "net.tcp://localhost:8090/EasyDNS";
        /// <summary>
        /// Initializes a new instance of the <see cref="EasyDNSServiceHost"/> class.
        /// </summary>
        public EasyDNSServiceHost()
            : base(typeof(WCFServices.DNSService), new Uri(EndPointAddress))
        {
            Type contractType = typeof(WCFServices.IDNSService);
            Type serviceType = typeof(WCFServices.DNSService);
            Uri baseAddress = new Uri(EndPointAddress);

            // Create the ServiceHost and add an endpoint, then start
            // the service.
            this.AddServiceEndpoint(contractType, new NetTcpBinding(), "EasyDNSService");

            //enable metadata
            ServiceMetadataBehavior smb = new ServiceMetadataBehavior();
            this.Description.Behaviors.Add(smb);

            this.Description.Behaviors.Add(new ServiceDiscoveryBehavior());
            this.AddServiceEndpoint(new UdpDiscoveryEndpoint());            
        }

        public EasyDNSServiceHost(EventHandler stateChangedHandler)
            : this()
        {
            if (stateChangedHandler == null)
                throw new ArgumentNullException(nameof(stateChangedHandler));

            this.Opening += stateChangedHandler;
            this.Opened += stateChangedHandler;
            this.Closing += stateChangedHandler;
            this.Closed += stateChangedHandler;
        }
    }
}
