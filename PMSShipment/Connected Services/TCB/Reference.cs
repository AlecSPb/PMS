﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.42000
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

namespace PMSShipment.TCB {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="DcDeliveryItem", Namespace="http://schemas.datacontract.org/2004/07/PMSWCFService.DataContracts")]
    [System.SerializableAttribute()]
    public partial class DcDeliveryItem : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string AbbrField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string BondingPOField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string CompositionField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.DateTime CreateTimeField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string CreatorField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string CustomerField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string DefectsField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Guid DeliveryIDField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string DeliveryTypeField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string DetailRecordField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string DimensionField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string DimensionActualField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Guid IDField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int OrderNumberField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string POField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int PackNumberField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string PositionField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string ProductIDField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string ProductTypeField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string RemarkField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string StateField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string TCBRemarkField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string TCBStateField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string TrackingHistoryField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string WeightField;
        
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
        public string Abbr {
            get {
                return this.AbbrField;
            }
            set {
                if ((object.ReferenceEquals(this.AbbrField, value) != true)) {
                    this.AbbrField = value;
                    this.RaisePropertyChanged("Abbr");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string BondingPO {
            get {
                return this.BondingPOField;
            }
            set {
                if ((object.ReferenceEquals(this.BondingPOField, value) != true)) {
                    this.BondingPOField = value;
                    this.RaisePropertyChanged("BondingPO");
                }
            }
        }
        
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
        public string Defects {
            get {
                return this.DefectsField;
            }
            set {
                if ((object.ReferenceEquals(this.DefectsField, value) != true)) {
                    this.DefectsField = value;
                    this.RaisePropertyChanged("Defects");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Guid DeliveryID {
            get {
                return this.DeliveryIDField;
            }
            set {
                if ((this.DeliveryIDField.Equals(value) != true)) {
                    this.DeliveryIDField = value;
                    this.RaisePropertyChanged("DeliveryID");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string DeliveryType {
            get {
                return this.DeliveryTypeField;
            }
            set {
                if ((object.ReferenceEquals(this.DeliveryTypeField, value) != true)) {
                    this.DeliveryTypeField = value;
                    this.RaisePropertyChanged("DeliveryType");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string DetailRecord {
            get {
                return this.DetailRecordField;
            }
            set {
                if ((object.ReferenceEquals(this.DetailRecordField, value) != true)) {
                    this.DetailRecordField = value;
                    this.RaisePropertyChanged("DetailRecord");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Dimension {
            get {
                return this.DimensionField;
            }
            set {
                if ((object.ReferenceEquals(this.DimensionField, value) != true)) {
                    this.DimensionField = value;
                    this.RaisePropertyChanged("Dimension");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string DimensionActual {
            get {
                return this.DimensionActualField;
            }
            set {
                if ((object.ReferenceEquals(this.DimensionActualField, value) != true)) {
                    this.DimensionActualField = value;
                    this.RaisePropertyChanged("DimensionActual");
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
        public int OrderNumber {
            get {
                return this.OrderNumberField;
            }
            set {
                if ((this.OrderNumberField.Equals(value) != true)) {
                    this.OrderNumberField = value;
                    this.RaisePropertyChanged("OrderNumber");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string PO {
            get {
                return this.POField;
            }
            set {
                if ((object.ReferenceEquals(this.POField, value) != true)) {
                    this.POField = value;
                    this.RaisePropertyChanged("PO");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int PackNumber {
            get {
                return this.PackNumberField;
            }
            set {
                if ((this.PackNumberField.Equals(value) != true)) {
                    this.PackNumberField = value;
                    this.RaisePropertyChanged("PackNumber");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Position {
            get {
                return this.PositionField;
            }
            set {
                if ((object.ReferenceEquals(this.PositionField, value) != true)) {
                    this.PositionField = value;
                    this.RaisePropertyChanged("Position");
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
        public string ProductType {
            get {
                return this.ProductTypeField;
            }
            set {
                if ((object.ReferenceEquals(this.ProductTypeField, value) != true)) {
                    this.ProductTypeField = value;
                    this.RaisePropertyChanged("ProductType");
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
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string TCBRemark {
            get {
                return this.TCBRemarkField;
            }
            set {
                if ((object.ReferenceEquals(this.TCBRemarkField, value) != true)) {
                    this.TCBRemarkField = value;
                    this.RaisePropertyChanged("TCBRemark");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string TCBState {
            get {
                return this.TCBStateField;
            }
            set {
                if ((object.ReferenceEquals(this.TCBStateField, value) != true)) {
                    this.TCBStateField = value;
                    this.RaisePropertyChanged("TCBState");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string TrackingHistory {
            get {
                return this.TrackingHistoryField;
            }
            set {
                if ((object.ReferenceEquals(this.TrackingHistoryField, value) != true)) {
                    this.TrackingHistoryField = value;
                    this.RaisePropertyChanged("TrackingHistory");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Weight {
            get {
                return this.WeightField;
            }
            set {
                if ((object.ReferenceEquals(this.WeightField, value) != true)) {
                    this.WeightField = value;
                    this.RaisePropertyChanged("Weight");
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
    [System.Runtime.Serialization.DataContractAttribute(Name="DcDelivery", Namespace="http://schemas.datacontract.org/2004/07/PMSWCFService.DataContracts")]
    [System.SerializableAttribute()]
    public partial class DcDelivery : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string AddressField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string CountryField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.DateTime CreateTimeField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string CreatorField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.DateTime CustomerSignedDateField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string CustomerSignedDetailsField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string DeliveryExpressField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string DeliveryNameField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string DeliveryNumberField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.DateTime FinishTimeField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Guid IDField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string InvoiceNumberField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private bool IsCustomerSignedField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string LastCheckIDCollectionField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.DateTime LastUpdateTimeField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string PackageInformationField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string PackageTypeField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string PackageWeightField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string ReceiverField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string RemarkField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.DateTime ShipTimeField;
        
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
        public string Address {
            get {
                return this.AddressField;
            }
            set {
                if ((object.ReferenceEquals(this.AddressField, value) != true)) {
                    this.AddressField = value;
                    this.RaisePropertyChanged("Address");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Country {
            get {
                return this.CountryField;
            }
            set {
                if ((object.ReferenceEquals(this.CountryField, value) != true)) {
                    this.CountryField = value;
                    this.RaisePropertyChanged("Country");
                }
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
        public System.DateTime CustomerSignedDate {
            get {
                return this.CustomerSignedDateField;
            }
            set {
                if ((this.CustomerSignedDateField.Equals(value) != true)) {
                    this.CustomerSignedDateField = value;
                    this.RaisePropertyChanged("CustomerSignedDate");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string CustomerSignedDetails {
            get {
                return this.CustomerSignedDetailsField;
            }
            set {
                if ((object.ReferenceEquals(this.CustomerSignedDetailsField, value) != true)) {
                    this.CustomerSignedDetailsField = value;
                    this.RaisePropertyChanged("CustomerSignedDetails");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string DeliveryExpress {
            get {
                return this.DeliveryExpressField;
            }
            set {
                if ((object.ReferenceEquals(this.DeliveryExpressField, value) != true)) {
                    this.DeliveryExpressField = value;
                    this.RaisePropertyChanged("DeliveryExpress");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string DeliveryName {
            get {
                return this.DeliveryNameField;
            }
            set {
                if ((object.ReferenceEquals(this.DeliveryNameField, value) != true)) {
                    this.DeliveryNameField = value;
                    this.RaisePropertyChanged("DeliveryName");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string DeliveryNumber {
            get {
                return this.DeliveryNumberField;
            }
            set {
                if ((object.ReferenceEquals(this.DeliveryNumberField, value) != true)) {
                    this.DeliveryNumberField = value;
                    this.RaisePropertyChanged("DeliveryNumber");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.DateTime FinishTime {
            get {
                return this.FinishTimeField;
            }
            set {
                if ((this.FinishTimeField.Equals(value) != true)) {
                    this.FinishTimeField = value;
                    this.RaisePropertyChanged("FinishTime");
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
        public string InvoiceNumber {
            get {
                return this.InvoiceNumberField;
            }
            set {
                if ((object.ReferenceEquals(this.InvoiceNumberField, value) != true)) {
                    this.InvoiceNumberField = value;
                    this.RaisePropertyChanged("InvoiceNumber");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public bool IsCustomerSigned {
            get {
                return this.IsCustomerSignedField;
            }
            set {
                if ((this.IsCustomerSignedField.Equals(value) != true)) {
                    this.IsCustomerSignedField = value;
                    this.RaisePropertyChanged("IsCustomerSigned");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string LastCheckIDCollection {
            get {
                return this.LastCheckIDCollectionField;
            }
            set {
                if ((object.ReferenceEquals(this.LastCheckIDCollectionField, value) != true)) {
                    this.LastCheckIDCollectionField = value;
                    this.RaisePropertyChanged("LastCheckIDCollection");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.DateTime LastUpdateTime {
            get {
                return this.LastUpdateTimeField;
            }
            set {
                if ((this.LastUpdateTimeField.Equals(value) != true)) {
                    this.LastUpdateTimeField = value;
                    this.RaisePropertyChanged("LastUpdateTime");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string PackageInformation {
            get {
                return this.PackageInformationField;
            }
            set {
                if ((object.ReferenceEquals(this.PackageInformationField, value) != true)) {
                    this.PackageInformationField = value;
                    this.RaisePropertyChanged("PackageInformation");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string PackageType {
            get {
                return this.PackageTypeField;
            }
            set {
                if ((object.ReferenceEquals(this.PackageTypeField, value) != true)) {
                    this.PackageTypeField = value;
                    this.RaisePropertyChanged("PackageType");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string PackageWeight {
            get {
                return this.PackageWeightField;
            }
            set {
                if ((object.ReferenceEquals(this.PackageWeightField, value) != true)) {
                    this.PackageWeightField = value;
                    this.RaisePropertyChanged("PackageWeight");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Receiver {
            get {
                return this.ReceiverField;
            }
            set {
                if ((object.ReferenceEquals(this.ReceiverField, value) != true)) {
                    this.ReceiverField = value;
                    this.RaisePropertyChanged("Receiver");
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
        public System.DateTime ShipTime {
            get {
                return this.ShipTimeField;
            }
            set {
                if ((this.ShipTimeField.Equals(value) != true)) {
                    this.ShipTimeField = value;
                    this.RaisePropertyChanged("ShipTime");
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
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="TCB.ITCBService")]
    public interface ITCBService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITCBService/GetDeliveryItemTCB", ReplyAction="http://tempuri.org/ITCBService/GetDeliveryItemTCBResponse")]
        PMSShipment.TCB.DcDeliveryItem[] GetDeliveryItemTCB(int s, int t, string productid, string composition, string po, string customer, string bondingpo, string state);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITCBService/GetDeliveryItemTCB", ReplyAction="http://tempuri.org/ITCBService/GetDeliveryItemTCBResponse")]
        System.Threading.Tasks.Task<PMSShipment.TCB.DcDeliveryItem[]> GetDeliveryItemTCBAsync(int s, int t, string productid, string composition, string po, string customer, string bondingpo, string state);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITCBService/GetDeliveryItemTCBCount", ReplyAction="http://tempuri.org/ITCBService/GetDeliveryItemTCBCountResponse")]
        int GetDeliveryItemTCBCount(string productid, string composition, string po, string customer, string bondingpo, string state);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITCBService/GetDeliveryItemTCBCount", ReplyAction="http://tempuri.org/ITCBService/GetDeliveryItemTCBCountResponse")]
        System.Threading.Tasks.Task<int> GetDeliveryItemTCBCountAsync(string productid, string composition, string po, string customer, string bondingpo, string state);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITCBService/AddDeliveryItemTCB", ReplyAction="http://tempuri.org/ITCBService/AddDeliveryItemTCBResponse")]
        void AddDeliveryItemTCB(PMSShipment.TCB.DcDeliveryItem model);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITCBService/AddDeliveryItemTCB", ReplyAction="http://tempuri.org/ITCBService/AddDeliveryItemTCBResponse")]
        System.Threading.Tasks.Task AddDeliveryItemTCBAsync(PMSShipment.TCB.DcDeliveryItem model);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITCBService/UpdateDeliveryItemTCB", ReplyAction="http://tempuri.org/ITCBService/UpdateDeliveryItemTCBResponse")]
        void UpdateDeliveryItemTCB(PMSShipment.TCB.DcDeliveryItem model);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITCBService/UpdateDeliveryItemTCB", ReplyAction="http://tempuri.org/ITCBService/UpdateDeliveryItemTCBResponse")]
        System.Threading.Tasks.Task UpdateDeliveryItemTCBAsync(PMSShipment.TCB.DcDeliveryItem model);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITCBService/GetDelivery", ReplyAction="http://tempuri.org/ITCBService/GetDeliveryResponse")]
        PMSShipment.TCB.DcDelivery[] GetDelivery(int s, int t, string deliveryname);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITCBService/GetDelivery", ReplyAction="http://tempuri.org/ITCBService/GetDeliveryResponse")]
        System.Threading.Tasks.Task<PMSShipment.TCB.DcDelivery[]> GetDeliveryAsync(int s, int t, string deliveryname);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITCBService/GetDeliveryCount", ReplyAction="http://tempuri.org/ITCBService/GetDeliveryCountResponse")]
        int GetDeliveryCount(string deliveryname);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITCBService/GetDeliveryCount", ReplyAction="http://tempuri.org/ITCBService/GetDeliveryCountResponse")]
        System.Threading.Tasks.Task<int> GetDeliveryCountAsync(string deliveryname);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITCBService/GetDeliveryItemTCBByDeliveryID", ReplyAction="http://tempuri.org/ITCBService/GetDeliveryItemTCBByDeliveryIDResponse")]
        PMSShipment.TCB.DcDeliveryItem[] GetDeliveryItemTCBByDeliveryID(System.Guid id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITCBService/GetDeliveryItemTCBByDeliveryID", ReplyAction="http://tempuri.org/ITCBService/GetDeliveryItemTCBByDeliveryIDResponse")]
        System.Threading.Tasks.Task<PMSShipment.TCB.DcDeliveryItem[]> GetDeliveryItemTCBByDeliveryIDAsync(System.Guid id);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface ITCBServiceChannel : PMSShipment.TCB.ITCBService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class TCBServiceClient : System.ServiceModel.ClientBase<PMSShipment.TCB.ITCBService>, PMSShipment.TCB.ITCBService {
        
        public TCBServiceClient() {
        }
        
        public TCBServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public TCBServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public TCBServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public TCBServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public PMSShipment.TCB.DcDeliveryItem[] GetDeliveryItemTCB(int s, int t, string productid, string composition, string po, string customer, string bondingpo, string state) {
            return base.Channel.GetDeliveryItemTCB(s, t, productid, composition, po, customer, bondingpo, state);
        }
        
        public System.Threading.Tasks.Task<PMSShipment.TCB.DcDeliveryItem[]> GetDeliveryItemTCBAsync(int s, int t, string productid, string composition, string po, string customer, string bondingpo, string state) {
            return base.Channel.GetDeliveryItemTCBAsync(s, t, productid, composition, po, customer, bondingpo, state);
        }
        
        public int GetDeliveryItemTCBCount(string productid, string composition, string po, string customer, string bondingpo, string state) {
            return base.Channel.GetDeliveryItemTCBCount(productid, composition, po, customer, bondingpo, state);
        }
        
        public System.Threading.Tasks.Task<int> GetDeliveryItemTCBCountAsync(string productid, string composition, string po, string customer, string bondingpo, string state) {
            return base.Channel.GetDeliveryItemTCBCountAsync(productid, composition, po, customer, bondingpo, state);
        }
        
        public void AddDeliveryItemTCB(PMSShipment.TCB.DcDeliveryItem model) {
            base.Channel.AddDeliveryItemTCB(model);
        }
        
        public System.Threading.Tasks.Task AddDeliveryItemTCBAsync(PMSShipment.TCB.DcDeliveryItem model) {
            return base.Channel.AddDeliveryItemTCBAsync(model);
        }
        
        public void UpdateDeliveryItemTCB(PMSShipment.TCB.DcDeliveryItem model) {
            base.Channel.UpdateDeliveryItemTCB(model);
        }
        
        public System.Threading.Tasks.Task UpdateDeliveryItemTCBAsync(PMSShipment.TCB.DcDeliveryItem model) {
            return base.Channel.UpdateDeliveryItemTCBAsync(model);
        }
        
        public PMSShipment.TCB.DcDelivery[] GetDelivery(int s, int t, string deliveryname) {
            return base.Channel.GetDelivery(s, t, deliveryname);
        }
        
        public System.Threading.Tasks.Task<PMSShipment.TCB.DcDelivery[]> GetDeliveryAsync(int s, int t, string deliveryname) {
            return base.Channel.GetDeliveryAsync(s, t, deliveryname);
        }
        
        public int GetDeliveryCount(string deliveryname) {
            return base.Channel.GetDeliveryCount(deliveryname);
        }
        
        public System.Threading.Tasks.Task<int> GetDeliveryCountAsync(string deliveryname) {
            return base.Channel.GetDeliveryCountAsync(deliveryname);
        }
        
        public PMSShipment.TCB.DcDeliveryItem[] GetDeliveryItemTCBByDeliveryID(System.Guid id) {
            return base.Channel.GetDeliveryItemTCBByDeliveryID(id);
        }
        
        public System.Threading.Tasks.Task<PMSShipment.TCB.DcDeliveryItem[]> GetDeliveryItemTCBByDeliveryIDAsync(System.Guid id) {
            return base.Channel.GetDeliveryItemTCBByDeliveryIDAsync(id);
        }
    }
}
