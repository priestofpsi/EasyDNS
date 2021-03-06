﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Discovery;
using System.Text;
using System.Threading.Tasks;

namespace theDiary.EasyDNS.Windows.Service
{
    public class EasyDNSConsole
    {
        private volatile EasyDNSServiceHost host;
        private readonly object syncObject = new object();

        public void Start(string[] args)
        {
            lock (this.syncObject)
            {
                this.host = new EasyDNSServiceHost(this.StateChangeHandler);
                this.host.Open();

                Program.EventLogEntryDelegate = this.WriteEventLog;
            }
        }

        private void WriteEventLog(string message,
            System.Diagnostics.EventLogEntryType type = System.Diagnostics.EventLogEntryType.Information,
            int eventID = default(int),
            short category = default(short))
        {
            if (Program.EventLogEntryDelegate == null)
                return;

            Console.WriteLine(message);
        }
        private void StateChangeHandler(object sender, EventArgs e)
        {
            ServiceHost host = (ServiceHost)sender;
            Console.WriteLine($"{host.Description.Name} Service {host.State} @ {host.BaseAddresses.FirstOrDefault().ToString()}...");
        }
        public void Stop()
        {
            lock (this.syncObject)
            {
                this.host.Close();
                this.host = null;
            }
        }

        public CommunicationState State
        {
            get
            {
                if (this.host == null)
                    return CommunicationState.Closed;
                return this.host.State;
            }
        }
    }
}
