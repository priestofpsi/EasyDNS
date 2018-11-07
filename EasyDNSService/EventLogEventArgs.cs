using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace theDiary.EasyDNS.Windows.Service
{
    public delegate void WriteEventLogEntryHandler(object sender, EventLogEventArgs e);
    public delegate void WriteEventLogEntryDelegate(string message, EventLogEntryType type, int eventID, short category);

    public class EventLogEventArgs
        : EventArgs
    {
        #region Constructors
        public EventLogEventArgs(string message)
            : base()
        {
            this.Message = message;
        }

        public EventLogEventArgs(string message, EventLogEntryType type)
            : base()
        {
            this.Message = message;
            this.Type = type;
        }

        public EventLogEventArgs(string message, EventLogEntryType type, int eventID)
            : base()
        {
            this.Message = message;
            this.Type = type;
            this.EventID = eventID;
        }

        public EventLogEventArgs(string message, EventLogEntryType type, int eventID, short category)
            : base()
        {
            this.Message = message;
            this.Type = type;
            this.EventID = eventID;
            this.Category = category;
        }
        #endregion

        #region Public Properties
        public string Message { get; private set; }

        public EventLogEntryType Type { get; private set; }

        public int EventID { get; private set; }

        public short Category { get; private set; }
        #endregion

        #region Public Methods & Functions
        public void WriteEventEntry(EventLog eventLog, bool throwError = false)
        {
            if (eventLog != null)
                eventLog.WriteEntry(this.Message, this.Type, this.EventID, this.Category);
            if (!throwError)
                return;

            throw new ArgumentNullException(nameof(eventLog));
        }
        #endregion
    }
}
