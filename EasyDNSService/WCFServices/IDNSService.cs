using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace theDiary.EasyDNS.Windows.Service.WCFServices
{
    [ServiceContract(CallbackContract = typeof(IDNSServiceCallback), SessionMode = SessionMode.Required)]
    
    public interface IDNSService
    {
        [OperationContract(IsOneWay =true)]
        void ChangeDNS(DNSConfiguration newConfiguration, byte[] macAddress);

        [OperationContract]
        DNSOperationResult GetDNSConfiguration(byte[] macAddress);

        [OperationContract]
        NetworkAdapterInfo[] GetNetworkAdapters();

    }

    public interface IDNSServiceCallback
    {
        [OperationContract]
        void OnNetworkAdaptersChanged(NetworkAdapterInfo[] devices);

        [OperationContract]
        void OnNetworkConfigurationChanged(NetworkAdapterInfo originalConfiguration, NetworkAdapterInfo newConfiguration);

        [OperationContract]
        void OnPublicIPAddressChanged(IPAddress originalIPAddress, IPAddress newIPAddress);
    }
}
