﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace theDiary.EasyDNS.Windows.DNSService {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="DNSConfiguration", Namespace="http://schemas.datacontract.org/2004/07/theDiary.EasyDNS.Windows.Service")]
    [System.SerializableAttribute()]
    public partial class DNSConfiguration : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Net.IPAddress PrimaryDNSField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Net.IPAddress SecondaryDNSField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Net.IPAddress PrimaryDNS {
            get {
                return this.PrimaryDNSField;
            }
            set {
                if ((object.ReferenceEquals(this.PrimaryDNSField, value) != true)) {
                    this.PrimaryDNSField = value;
                    this.RaisePropertyChanged("PrimaryDNS");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Net.IPAddress SecondaryDNS {
            get {
                return this.SecondaryDNSField;
            }
            set {
                if ((object.ReferenceEquals(this.SecondaryDNSField, value) != true)) {
                    this.SecondaryDNSField = value;
                    this.RaisePropertyChanged("SecondaryDNS");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="OperationResultOfDNSConfigurationo4iSXZRF", Namespace="http://schemas.datacontract.org/2004/07/theDiary.EasyDNS.Windows.Service")]
    [System.SerializableAttribute()]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(theDiary.EasyDNS.Windows.DNSService.DNSOperationResult))]
    public partial class OperationResultOfDNSConfigurationo4iSXZRF : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string MessageField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private theDiary.EasyDNS.Windows.DNSService.DNSConfiguration ResultField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private bool SuccessField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Message {
            get {
                return this.MessageField;
            }
            set {
                if ((object.ReferenceEquals(this.MessageField, value) != true)) {
                    this.MessageField = value;
                    this.RaisePropertyChanged("Message");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public theDiary.EasyDNS.Windows.DNSService.DNSConfiguration Result {
            get {
                return this.ResultField;
            }
            set {
                if ((object.ReferenceEquals(this.ResultField, value) != true)) {
                    this.ResultField = value;
                    this.RaisePropertyChanged("Result");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public bool Success {
            get {
                return this.SuccessField;
            }
            set {
                if ((this.SuccessField.Equals(value) != true)) {
                    this.SuccessField = value;
                    this.RaisePropertyChanged("Success");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="DNSOperationResult", Namespace="http://schemas.datacontract.org/2004/07/theDiary.EasyDNS.Windows.Service")]
    [System.SerializableAttribute()]
    public partial class DNSOperationResult : theDiary.EasyDNS.Windows.DNSService.OperationResultOfDNSConfigurationo4iSXZRF {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="NetworkAdapterInfo", Namespace="http://schemas.datacontract.org/2004/07/theDiary.EasyDNS.Windows.Service")]
    [System.SerializableAttribute()]
    public partial class NetworkAdapterInfo : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private theDiary.EasyDNS.Windows.DNSService.DNSConfiguration DNSConfigurationField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string DeviceNameField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Net.IPAddress IPAddressField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private byte[] MACAddressField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public theDiary.EasyDNS.Windows.DNSService.DNSConfiguration DNSConfiguration {
            get {
                return this.DNSConfigurationField;
            }
            set {
                if ((object.ReferenceEquals(this.DNSConfigurationField, value) != true)) {
                    this.DNSConfigurationField = value;
                    this.RaisePropertyChanged("DNSConfiguration");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string DeviceName {
            get {
                return this.DeviceNameField;
            }
            set {
                if ((object.ReferenceEquals(this.DeviceNameField, value) != true)) {
                    this.DeviceNameField = value;
                    this.RaisePropertyChanged("DeviceName");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Net.IPAddress IPAddress {
            get {
                return this.IPAddressField;
            }
            set {
                if ((object.ReferenceEquals(this.IPAddressField, value) != true)) {
                    this.IPAddressField = value;
                    this.RaisePropertyChanged("IPAddress");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public byte[] MACAddress {
            get {
                return this.MACAddressField;
            }
            set {
                if ((object.ReferenceEquals(this.MACAddressField, value) != true)) {
                    this.MACAddressField = value;
                    this.RaisePropertyChanged("MACAddress");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="DNSService.IDNSService", CallbackContract=typeof(theDiary.EasyDNS.Windows.DNSService.IDNSServiceCallback), SessionMode=System.ServiceModel.SessionMode.Required)]
    public interface IDNSService {
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IDNSService/ChangeDNS")]
        void ChangeDNS(theDiary.EasyDNS.Windows.DNSService.DNSConfiguration newConfiguration, byte[] macAddress);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IDNSService/ChangeDNS")]
        System.Threading.Tasks.Task ChangeDNSAsync(theDiary.EasyDNS.Windows.DNSService.DNSConfiguration newConfiguration, byte[] macAddress);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IDNSService/GetDNSConfiguration", ReplyAction="http://tempuri.org/IDNSService/GetDNSConfigurationResponse")]
        theDiary.EasyDNS.Windows.DNSService.DNSOperationResult GetDNSConfiguration(byte[] macAddress);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IDNSService/GetDNSConfiguration", ReplyAction="http://tempuri.org/IDNSService/GetDNSConfigurationResponse")]
        System.Threading.Tasks.Task<theDiary.EasyDNS.Windows.DNSService.DNSOperationResult> GetDNSConfigurationAsync(byte[] macAddress);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IDNSService/GetNetworkAdapters", ReplyAction="http://tempuri.org/IDNSService/GetNetworkAdaptersResponse")]
        System.Collections.Generic.List<theDiary.EasyDNS.Windows.DNSService.NetworkAdapterInfo> GetNetworkAdapters();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IDNSService/GetNetworkAdapters", ReplyAction="http://tempuri.org/IDNSService/GetNetworkAdaptersResponse")]
        System.Threading.Tasks.Task<System.Collections.Generic.List<theDiary.EasyDNS.Windows.DNSService.NetworkAdapterInfo>> GetNetworkAdaptersAsync();
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IDNSServiceCallback {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IDNSService/OnNetworkAdaptersChanged", ReplyAction="http://tempuri.org/IDNSService/OnNetworkAdaptersChangedResponse")]
        void OnNetworkAdaptersChanged(System.Collections.Generic.List<theDiary.EasyDNS.Windows.DNSService.NetworkAdapterInfo> devices);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IDNSService/OnNetworkConfigurationChanged", ReplyAction="http://tempuri.org/IDNSService/OnNetworkConfigurationChangedResponse")]
        void OnNetworkConfigurationChanged(theDiary.EasyDNS.Windows.DNSService.NetworkAdapterInfo originalConfiguration, theDiary.EasyDNS.Windows.DNSService.NetworkAdapterInfo newConfiguration);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IDNSService/OnPublicIPAddressChanged", ReplyAction="http://tempuri.org/IDNSService/OnPublicIPAddressChangedResponse")]
        void OnPublicIPAddressChanged(System.Net.IPAddress originalIPAddress, System.Net.IPAddress newIPAddress);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IDNSServiceChannel : theDiary.EasyDNS.Windows.DNSService.IDNSService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class DNSServiceClient : System.ServiceModel.DuplexClientBase<theDiary.EasyDNS.Windows.DNSService.IDNSService>, theDiary.EasyDNS.Windows.DNSService.IDNSService {
        
        public DNSServiceClient(System.ServiceModel.InstanceContext callbackInstance) : 
                base(callbackInstance) {
        }
        
        public DNSServiceClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName) : 
                base(callbackInstance, endpointConfigurationName) {
        }
        
        public DNSServiceClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName, string remoteAddress) : 
                base(callbackInstance, endpointConfigurationName, remoteAddress) {
        }
        
        public DNSServiceClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(callbackInstance, endpointConfigurationName, remoteAddress) {
        }
        
        public DNSServiceClient(System.ServiceModel.InstanceContext callbackInstance, System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(callbackInstance, binding, remoteAddress) {
        }
        
        public void ChangeDNS(theDiary.EasyDNS.Windows.DNSService.DNSConfiguration newConfiguration, byte[] macAddress) {
            base.Channel.ChangeDNS(newConfiguration, macAddress);
        }
        
        public System.Threading.Tasks.Task ChangeDNSAsync(theDiary.EasyDNS.Windows.DNSService.DNSConfiguration newConfiguration, byte[] macAddress) {
            return base.Channel.ChangeDNSAsync(newConfiguration, macAddress);
        }
        
        public theDiary.EasyDNS.Windows.DNSService.DNSOperationResult GetDNSConfiguration(byte[] macAddress) {
            return base.Channel.GetDNSConfiguration(macAddress);
        }
        
        public System.Threading.Tasks.Task<theDiary.EasyDNS.Windows.DNSService.DNSOperationResult> GetDNSConfigurationAsync(byte[] macAddress) {
            return base.Channel.GetDNSConfigurationAsync(macAddress);
        }
        
        public System.Collections.Generic.List<theDiary.EasyDNS.Windows.DNSService.NetworkAdapterInfo> GetNetworkAdapters() {
            return base.Channel.GetNetworkAdapters();
        }
        
        public System.Threading.Tasks.Task<System.Collections.Generic.List<theDiary.EasyDNS.Windows.DNSService.NetworkAdapterInfo>> GetNetworkAdaptersAsync() {
            return base.Channel.GetNetworkAdaptersAsync();
        }
    }
}
