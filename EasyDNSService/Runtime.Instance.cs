using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using theDiary.EasyDNS.Windows.Service.WCFServices;

namespace theDiary.EasyDNS.Windows.Service
{
    internal partial class Runtime
    {
        #region Private Declarations
        private volatile Dictionary<string, IDNSServiceCallback> callbacks = new Dictionary<string, IDNSServiceCallback>();
        private volatile IEnumerable<WirelessNetworkInterface> wirelessInterfaces;
        #endregion

        public IEnumerable<WirelessNetworkInterface> WirelessDevices
        {
            get
            {

                if (this.wirelessInterfaces.IsNullOrEmpty())
                    this.wirelessInterfaces = DNSHelper.GetWirelessDevices();

                return this.wirelessInterfaces;
            }
        }

        public IEnumerable<IDNSServiceCallback> Callbacks
        {
            get
            {
                if (this.callbacks == null)
                    this.callbacks = new Dictionary<string, IDNSServiceCallback>();

                return this.callbacks.Values;
            }
        }

        public void RegisterSession(OperationContext context)
        {
            if (context == null || this.SessionRegistered(context.SessionId))
                return;

            this.RegisterSession(context.SessionId, context.GetCallbackChannel<IDNSServiceCallback>());
        }

        public void RegisterSession(string sessionID, IDNSServiceCallback callback)
        {
            if (this.SessionRegistered(sessionID) || callback == null)
                return;

            this.callbacks.Add(sessionID, callback);
        }

        public void UnregisterSession(OperationContext context)
        {
            if (context == null)
                return;

            this.UnregisterSession(context.SessionId);
        }

        public void UnregisterSession(string sessionId)
        {
            if (!this.callbacks.ContainsKey(sessionId))
                return;

            this.callbacks.Remove(sessionId);
        }

        public bool SessionRegistered(string sessionID)
        {
            if (string.IsNullOrWhiteSpace(sessionID))
                return false;

            return this.callbacks.ContainsKey(sessionID);
        }

        internal void ResetWirelessInterfaces()
        {
            this.wirelessInterfaces = null;
        }
    }
}
