﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.42000
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

namespace PMSClient.Sample {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="DcSample", Namespace="http://schemas.datacontract.org/2004/07/PMSWCFService.DataContracts")]
    [System.SerializableAttribute()]
    public partial class DcSample : PMSClient.Sample.DcModelBase {
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string CompositionField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string CustomerField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string MoreInformationField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string MoreTestResultField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string OriginalRequirementField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string PMINumberField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string ProductIDField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string RemarkField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string SampleTypeField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string TestResultField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string TraceInformationField;
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Composition {
            get {
                return this.CompositionField;
            }
            set {
                if ((object.ReferenceEquals(this.CompositionField, value) != true)) {
                    this.CompositionField = value;
                    this.RaisePropertyChanged("Composition");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Customer {
            get {
                return this.CustomerField;
            }
            set {
                if ((object.ReferenceEquals(this.CustomerField, value) != true)) {
                    this.CustomerField = value;
                    this.RaisePropertyChanged("Customer");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string MoreInformation {
            get {
                return this.MoreInformationField;
            }
            set {
                if ((object.ReferenceEquals(this.MoreInformationField, value) != true)) {
                    this.MoreInformationField = value;
                    this.RaisePropertyChanged("MoreInformation");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string MoreTestResult {
            get {
                return this.MoreTestResultField;
            }
            set {
                if ((object.ReferenceEquals(this.MoreTestResultField, value) != true)) {
                    this.MoreTestResultField = value;
                    this.RaisePropertyChanged("MoreTestResult");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string OriginalRequirement {
            get {
                return this.OriginalRequirementField;
            }
            set {
                if ((object.ReferenceEquals(this.OriginalRequirementField, value) != true)) {
                    this.OriginalRequirementField = value;
                    this.RaisePropertyChanged("OriginalRequirement");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string PMINumber {
            get {
                return this.PMINumberField;
            }
            set {
                if ((object.ReferenceEquals(this.PMINumberField, value) != true)) {
                    this.PMINumberField = value;
                    this.RaisePropertyChanged("PMINumber");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string ProductID {
            get {
                return this.ProductIDField;
            }
            set {
                if ((object.ReferenceEquals(this.ProductIDField, value) != true)) {
                    this.ProductIDField = value;
                    this.RaisePropertyChanged("ProductID");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Remark {
            get {
                return this.RemarkField;
            }
            set {
                if ((object.ReferenceEquals(this.RemarkField, value) != true)) {
                    this.RemarkField = value;
                    this.RaisePropertyChanged("Remark");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string SampleType {
            get {
                return this.SampleTypeField;
            }
            set {
                if ((object.ReferenceEquals(this.SampleTypeField, value) != true)) {
                    this.SampleTypeField = value;
                    this.RaisePropertyChanged("SampleType");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string TestResult {
            get {
                return this.TestResultField;
            }
            set {
                if ((object.ReferenceEquals(this.TestResultField, value) != true)) {
                    this.TestResultField = value;
                    this.RaisePropertyChanged("TestResult");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string TraceInformation {
            get {
                return this.TraceInformationField;
            }
            set {
                if ((object.ReferenceEquals(this.TraceInformationField, value) != true)) {
                    this.TraceInformationField = value;
                    this.RaisePropertyChanged("TraceInformation");
                }
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="DcModelBase", Namespace="http://schemas.datacontract.org/2004/07/PMSWCFService.DataContracts")]
    [System.SerializableAttribute()]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(PMSClient.Sample.DcSample))]
    public partial class DcModelBase : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.DateTime CreateTimeField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string CreatorField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Guid IDField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string StateField;
        
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
        public System.DateTime CreateTime {
            get {
                return this.CreateTimeField;
            }
            set {
                if ((this.CreateTimeField.Equals(value) != true)) {
                    this.CreateTimeField = value;
                    this.RaisePropertyChanged("CreateTime");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Creator {
            get {
                return this.CreatorField;
            }
            set {
                if ((object.ReferenceEquals(this.CreatorField, value) != true)) {
                    this.CreatorField = value;
                    this.RaisePropertyChanged("Creator");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Guid ID {
            get {
                return this.IDField;
            }
            set {
                if ((this.IDField.Equals(value) != true)) {
                    this.IDField = value;
                    this.RaisePropertyChanged("ID");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string State {
            get {
                return this.StateField;
            }
            set {
                if ((object.ReferenceEquals(this.StateField, value) != true)) {
                    this.StateField = value;
                    this.RaisePropertyChanged("State");
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
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="Sample.ISampleService")]
    public interface ISampleService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISampleService/GetSampleAll", ReplyAction="http://tempuri.org/ISampleService/GetSampleAllResponse")]
        PMSClient.Sample.DcSample[] GetSampleAll(int s, int t, string productid, string composition, string sampletype);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISampleService/GetSampleAll", ReplyAction="http://tempuri.org/ISampleService/GetSampleAllResponse")]
        System.Threading.Tasks.Task<PMSClient.Sample.DcSample[]> GetSampleAllAsync(int s, int t, string productid, string composition, string sampletype);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISampleService/GetSampleAllCount", ReplyAction="http://tempuri.org/ISampleService/GetSampleAllCountResponse")]
        int GetSampleAllCount(string productid, string composition, string sampletype);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISampleService/GetSampleAllCount", ReplyAction="http://tempuri.org/ISampleService/GetSampleAllCountResponse")]
        System.Threading.Tasks.Task<int> GetSampleAllCountAsync(string productid, string composition, string sampletype);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISampleService/GetSampleByPMINumberCount", ReplyAction="http://tempuri.org/ISampleService/GetSampleByPMINumberCountResponse")]
        int GetSampleByPMINumberCount(string pminumber);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISampleService/GetSampleByPMINumberCount", ReplyAction="http://tempuri.org/ISampleService/GetSampleByPMINumberCountResponse")]
        System.Threading.Tasks.Task<int> GetSampleByPMINumberCountAsync(string pminumber);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISampleService/AddSample", ReplyAction="http://tempuri.org/ISampleService/AddSampleResponse")]
        void AddSample(PMSClient.Sample.DcSample model);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISampleService/AddSample", ReplyAction="http://tempuri.org/ISampleService/AddSampleResponse")]
        System.Threading.Tasks.Task AddSampleAsync(PMSClient.Sample.DcSample model);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISampleService/UpdateSample", ReplyAction="http://tempuri.org/ISampleService/UpdateSampleResponse")]
        void UpdateSample(PMSClient.Sample.DcSample model);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISampleService/UpdateSample", ReplyAction="http://tempuri.org/ISampleService/UpdateSampleResponse")]
        System.Threading.Tasks.Task UpdateSampleAsync(PMSClient.Sample.DcSample model);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface ISampleServiceChannel : PMSClient.Sample.ISampleService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class SampleServiceClient : System.ServiceModel.ClientBase<PMSClient.Sample.ISampleService>, PMSClient.Sample.ISampleService {
        
        public SampleServiceClient() {
        }
        
        public SampleServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public SampleServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public SampleServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public SampleServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public PMSClient.Sample.DcSample[] GetSampleAll(int s, int t, string productid, string composition, string sampletype) {
            return base.Channel.GetSampleAll(s, t, productid, composition, sampletype);
        }
        
        public System.Threading.Tasks.Task<PMSClient.Sample.DcSample[]> GetSampleAllAsync(int s, int t, string productid, string composition, string sampletype) {
            return base.Channel.GetSampleAllAsync(s, t, productid, composition, sampletype);
        }
        
        public int GetSampleAllCount(string productid, string composition, string sampletype) {
            return base.Channel.GetSampleAllCount(productid, composition, sampletype);
        }
        
        public System.Threading.Tasks.Task<int> GetSampleAllCountAsync(string productid, string composition, string sampletype) {
            return base.Channel.GetSampleAllCountAsync(productid, composition, sampletype);
        }
        
        public int GetSampleByPMINumberCount(string pminumber) {
            return base.Channel.GetSampleByPMINumberCount(pminumber);
        }
        
        public System.Threading.Tasks.Task<int> GetSampleByPMINumberCountAsync(string pminumber) {
            return base.Channel.GetSampleByPMINumberCountAsync(pminumber);
        }
        
        public void AddSample(PMSClient.Sample.DcSample model) {
            base.Channel.AddSample(model);
        }
        
        public System.Threading.Tasks.Task AddSampleAsync(PMSClient.Sample.DcSample model) {
            return base.Channel.AddSampleAsync(model);
        }
        
        public void UpdateSample(PMSClient.Sample.DcSample model) {
            base.Channel.UpdateSample(model);
        }
        
        public System.Threading.Tasks.Task UpdateSampleAsync(PMSClient.Sample.DcSample model) {
            return base.Channel.UpdateSampleAsync(model);
        }
    }
}