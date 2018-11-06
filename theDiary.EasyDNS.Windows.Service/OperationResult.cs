using System;
using System.Runtime.Serialization;

namespace theDiary.EasyDNS.Windows.Service
{
    [DataContract]
    public class OperationResult<T>
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="OperationResult"/> class.
        /// </summary>
        public OperationResult()
            : base()
        {

        }

        public OperationResult(T result)
            : this()
        {
            this.Result = result;
            this.Success = true;
        }

        public OperationResult(T result, string message)
            : this(result)
        {
            this.Message = message;
        }
        #endregion

        #region Public Properties
        /// <summary>
        /// Gets or sets a value indicating if the operation was successful or not.
        /// </summary>
        [DataMember(Name = "Success")]
        public bool Success { get; set; }

        /// <summary>
        /// Gets or sets a message associated with the operation.
        /// </summary>
        [DataMember(Name = "Message")]
        public string Message { get; set; }

        [DataMember(Name = "Result")]
        public virtual T Result { get; set; }
        #endregion
    }

    [DataContract(Name = "DNSOperationResult")]
    public class DNSOperationResult
        : OperationResult<DNSConfiguration>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OperationResult"/> class.
        /// </summary>
        public DNSOperationResult()
            : base()
        {

        }

        public DNSOperationResult(DNSConfiguration result, bool success = true)
            : this()
        {
            this.Result = result;
            this.Success = success;
        }

        public DNSOperationResult(DNSConfiguration result, string message, bool success = true)
            : this(result, success)
        {
            this.Message = message;
        }

        public DNSOperationResult(DNSConfiguration result, uint responseCode)
            : this(result)
        {
            bool success;
            this.Message = GetResponseMessage(responseCode, out success);
            this.Success = success;
        }

        public DNSOperationResult(DNSConfiguration result, Exception error)
            : this(error)
        {
            this.Result = result;
        }

        public DNSOperationResult(Exception error)
            : this()
        {
            this.Success = false;
            this.Message = error.Message;
        }

        [DataMember(Name = "Result")]
        public override DNSConfiguration Result
        {
            get
            {
                return base.Result;
            }
            set
            {
                base.Result = value;
            }
        }

        private static string GetResponseMessage(uint responseCode, out bool success)
        {
            success = responseCode <= 1;
            switch (responseCode)
            {
                case 0: return "Successful completion, no reboot required";
                case 1: return "Successful completion, reboot required.";
                case 64: return "Method not supported on this platform.";
                case 65: return "Unknown failure.";
                case 66: return "Invalid subnet mask.";
                case 67: return "An error occurred while processing an Instance that was returned.";
                case 68: return "Invalid input parameter.";
                case 69: return "More than 5 gateways specified.";
                case 70: return "Invalid IP address.";
                case 71: return "Invalid gateway IP address.";
                case 72: return "An error occurred while accessing the Registry for the requested information.";
                case 73: return "Invalid domain name.";
                case 74: return "Invalid host name.";
                case 75: return "No primary / secondary WINS server defined.";
                case 76: return "Invalid file.";
                case 77: return "Invalid system path(77)";
                case 78: return "File copy failed.";
                case 79: return "Invalid security parameter.";
                case 80: return "Unable to configure TCP / IP service.";
                case 81: return "Unable to configure DHCP service.";
                case 82: return "Unable to renew DHCP lease.";
                case 83: return "Unable to release DHCP lease.";
                case 84: return "IP not enabled on adapter.";
                case 85: return "IPX not enabled on adapter.";
                case 86: return "Frame / network number bounds error.";
                case 87: return "Invalid frame type.";
                case 88: return "Invalid network number.";
                case 89: return "Duplicate network number.";
                case 90: return "Parameter out of bounds.";
                case 91: return "Access denied.";
                case 92: return "Out of memory.";
                case 93: return "Already exists.";
                case 94: return "Path, file or object not found.";
                case 95: return "Unable to notify service.";
                case 96: return "Unable to notify DNS service.";
                case 97: return "Interface not configurable.";
                case 98: return "Not all DHCP leases could be released / renewed(98)";
                case 100: return "DHCP not enabled on adapter";
                default: return "Other/Unknown - Err: (101 4294967295)";
            }
        }
    }

}
