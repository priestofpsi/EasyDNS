using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Discovery;
using System.Text;
using System.Threading.Tasks;

namespace theDiary.EasyDNS.Windows.Service
{
    /// <summary>
    /// Provides a <see cref="ServiceHost"/> for the <see cref="WCFServices.DNSService"/>.
    /// </summary>
    public class EasyDNSServiceHost
        : ServiceHost
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="EasyDNSServiceHost"/> class.
        /// </summary>
        public EasyDNSServiceHost()
            : base(typeof(WCFServices.DNSService), new Uri(EasyDNSServiceHost.EndPointAddress))
        {
            Type contractType = typeof(WCFServices.IDNSService);
            Type serviceType = typeof(WCFServices.DNSService);
            Uri baseAddress = new Uri(EasyDNSServiceHost.EndPointAddress);
            Uri metaDataAddress = new Uri($"{EasyDNSServiceHost.EndPointAddress}/mex");

            this.AddServiceEndpoint(contractType, new NetTcpBinding(), baseAddress);
                        
            this.InitializeServiceMetaData(metaDataAddress);
            this.InitializeServiceDiscovery();            
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EasyDNSServiceHost"/> class, with the specified <paramref name="stateChangedHandler"/> delegate used to
        /// handle the notification of state changes.
        /// </summary>
        /// <param name="stateChangedHandler">A delegate instance of <see cref="EventHandler"/>.</param>
        /// <exception cref="ArgumentNullException">thrown if <paramref name="stateChangedHandler"/> is <c>Null</c>.</exception>
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
        #endregion

        #region Private Static Declarations
        private static readonly string EndPointAddress = $"net.tcp://{Environment.MachineName}:8090/EasyDNS";
        #endregion

        #region Private Methods & Functions
        /// <summary>
        /// Initializes Service MetaData (MEX) Behaviour and the required <see cref="EndPointAddress"/> for the <see cref="EasyDNSServiceHost"/>.
        /// </summary>
        /// <param name="metaDataAddress">The <see cref="Uri"/> instance used for the associated <see cref="EndPointAddress"/>.</param>
        private void InitializeServiceMetaData(Uri metaDataAddress = null)
        {
            if (metaDataAddress == null)
                metaDataAddress = new Uri($"{EasyDNSServiceHost.EndPointAddress}/mex");

            ServiceMetadataBehavior smb = new ServiceMetadataBehavior();
            this.Description.Behaviors.Add(smb);
            ContractDescription contractDescription = ContractDescription.GetContract(typeof(IMetadataExchange));
            contractDescription.Behaviors.Add(new ServiceMetadataContractBehavior(true));
            ServiceEndpoint mexEndpoint = new ServiceEndpoint(contractDescription, MetadataExchangeBindings.CreateMexTcpBinding(), new EndpointAddress(metaDataAddress));
            mexEndpoint.Name = "mexBehaviour";
            this.AddServiceEndpoint(mexEndpoint);
        }

        /// <summary>
        /// Initializes Service Discovery Behaviour and the required <see cref="EndPointAddress"/> for the <see cref="EasyDNSServiceHost"/>.
        /// </summary>
        private void InitializeServiceDiscovery()
        {
            this.Description.Behaviors.Add(new ServiceDiscoveryBehavior());
            this.AddServiceEndpoint(new UdpDiscoveryEndpoint());
        }
        #endregion
    }
}
