using System;
using System.ServiceModel;
using System.ServiceModel.Discovery;
using System.ServiceProcess;

namespace theDiary.EasyDNS.Windows.Service
{
    public partial class EasyDNSService : ServiceBase
    {
        public EasyDNSService()
        {
            InitializeComponent();

        }

        #region Private Declarations
        private volatile ServiceHost host;
        private readonly object syncObject = new object();
        #endregion

        protected override void OnStart(string[] args)
        {
            lock (this.syncObject)
            {
                this.host = new EasyDNSServiceHost();
                this.host.Open();

                Program.EventLogEntryDelegate = this.EventLog.WriteEntry;
            }
        }

        protected override void OnStop()
        {
            lock (this.syncObject)
            {
                this.host.Close();
                this.host = null;
            }
        }
    }
}
