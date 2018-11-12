using System;
using System.Collections.Generic;
using System.Net;
using System.Net.NetworkInformation;
using System.ServiceModel;

namespace theDiary.EasyDNS.Windows.Service.WCFServices
{
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class DNSService : IDNSService
    {
        #region Constructors
        public DNSService()
            : base()
        {

        }

        private void NetworkChange_NetworkAddressChanged(object sender, EventArgs e)
        {
            this.OnNetworkAdaptersChanged();
        }

        private void NetworkChange_NetworkAvailabilityChanged(object sender, System.Net.NetworkInformation.NetworkAvailabilityEventArgs e)
        {
            this.OnNetworkAdaptersChanged();
        }
        #endregion

        #region Private Declarations
        private static readonly object syncObject = new object();
        
        #endregion

        

        #region Public Methods & Functions
        public void ChangeDNS(DNSConfiguration newConfiguration, byte[] macAddress)
        {
            PhysicalAddress physicalAddress = new PhysicalAddress(macAddress);
            try
            {
                this.WriteEventLog($"{nameof(ChangeDNS)}({newConfiguration}, {macAddress})");
                this.VerifyAndInitializeCallback();
                NetworkAdapterInfo adapter = new NetworkAdapterInfo(physicalAddress);
                NetworkAdapterInfo originalConfiguration = adapter.Clone();
                uint responseCode = adapter.SetDNSConfiguration(newConfiguration);
                this.OnNetworkConfigurationChanged(adapter, new NetworkAdapterInfo(physicalAddress));
            }
            catch (Exception ex)
            {
                this.WriteEventLog($"Error Exeucuting: {nameof(ChangeDNS)}({newConfiguration}, {physicalAddress}): {ex.Message}", System.Diagnostics.EventLogEntryType.Error);
            }
        }

        public DNSOperationResult GetDNSConfiguration(byte[] macAddress)
        {
            PhysicalAddress physicalAddress = new PhysicalAddress(macAddress);
            try
            {
                this.WriteEventLog($"{nameof(GetDNSConfiguration)}(${macAddress})");
                this.VerifyAndInitializeCallback();
                NetworkAdapterInfo networkAdapterInfo = new NetworkAdapterInfo(physicalAddress);
                var returnValue = new DNSOperationResult(networkAdapterInfo.GetDNSConfiguration());

                return returnValue;
            }
            catch (Exception ex)
            {
                this.WriteEventLog($"Error Exeucuting: {nameof(GetDNSConfiguration)}({physicalAddress}): {ex.Message}", System.Diagnostics.EventLogEntryType.Error);
                return new DNSOperationResult(ex);
            }
        }

        public NetworkAdapterInfo[] GetNetworkAdapters()
        {
            try
            {
                this.WriteEventLog("GetNetworkAdapters");
                this.VerifyAndInitializeCallback();
                return NetworkAdapterInfo.GetAdapters();
            }
            catch (Exception ex)
            {
                this.WriteEventLog($"Error Exeucuting: {nameof(GetNetworkAdapters)}: {ex.Message}", System.Diagnostics.EventLogEntryType.Error);
                throw;
            }
        }
        #endregion
        private void OnNetworkAdaptersChanged()
        {
            this.VerifyAndInitializeCallback();
            Runtime.Instance.ResetWirelessInterfaces();
            lock (DNSService.syncObject)
            {
                foreach (var callback in Runtime.Instance.Callbacks)
                    callback.OnNetworkAdaptersChanged(this.GetNetworkAdapters());
            }
        }
        private void OnNetworkConfigurationChanged(NetworkAdapterInfo originalConfiguration, NetworkAdapterInfo newConfiguration)
        {
            this.VerifyAndInitializeCallback();
            lock (DNSService.syncObject)
            {
                foreach (var callback in Runtime.Instance.Callbacks)
                    callback.OnNetworkConfigurationChanged(originalConfiguration, newConfiguration);
            }
        }

        private void OnPublicIPAddressChanged(IPAddress originalIPAddress, IPAddress newIPAddress)
        {
            this.VerifyAndInitializeCallback();
            lock (DNSService.syncObject)
            {
                foreach (var callback in Runtime.Instance.Callbacks)
                    callback.OnPublicIPAddressChanged(originalIPAddress, newIPAddress);
            }
        }

        #region Private Methods & Functions
        private void VerifyAndInitializeCallback()
        {
            lock (syncObject)
            {
                var context = OperationContext.Current;
                if (context == null || Runtime.Instance.SessionRegistered(context.SessionId))
                    return;
                System.Net.NetworkInformation.NetworkChange.NetworkAvailabilityChanged += this.NetworkChange_NetworkAvailabilityChanged;
                System.Net.NetworkInformation.NetworkChange.NetworkAddressChanged += this.NetworkChange_NetworkAddressChanged;
                this.WriteEventLog("Initializing EasyDNS Callback");
                context.Channel.Faulted += (s, e) => Runtime.Instance.UnregisterSession(context.SessionId);
                context.Channel.Closing += (s, e) => Runtime.Instance.UnregisterSession(context.SessionId);
                Runtime.Instance.RegisterSession(context.SessionId, OperationContext.Current.GetCallbackChannel<IDNSServiceCallback>());
            }
        }

        private void WriteEventLog(string message,
            System.Diagnostics.EventLogEntryType type = System.Diagnostics.EventLogEntryType.Information,
            int eventID = default(int),
            short category = default(short))
        {
            if (Program.EventLogEntryDelegate == null)
                return;

            Program.EventLogEntryDelegate(message, type, eventID, category);
        }
        #endregion
    }

}
