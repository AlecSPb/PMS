using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace WcfService.Model
{
    [DataContract]
    public class MainOrder
    {
        [DataMember]
        public Guid MainOrderId { get; set; }
        [DataMember]
        public DateTime? OrderDate { get; set; }
        [DataMember]
        public string  CustomerName { get; set; }
        [DataMember]
        public string ProductName { get; set; }
        [DataMember]
        public string PO { get; set; }
        [DataMember]
        public string PMIWorkNumber { get; set; }
        [DataMember]
        public string ProductType { get; set; }
        [DataMember]
        public string Purity { get; set; }
        [DataMember]
        public string Shape { get; set; }
        [DataMember]
        public string Dimension { get; set; }
        [DataMember]
        public double? Quantity { get; set; }
        [DataMember]
        public string Unit { get; set; }
        [DataMember]
        public DateTime? DeliveryDateExpect { get; set; }
        [DataMember]
        public string Consignee { get; set; }
        [DataMember]
        public bool? IsDeliveryFinished { get; set; }
        [DataMember]
        public DateTime? DeliveryDateFact { get; set; }
        [DataMember]
        public bool? IsPlanFinished { get; set; }
        [DataMember]
        public int? Priority { get; set; }
        [DataMember]
        public string OrderState { get; set; }
        [DataMember]
        public string Remark { get; set; }
    }
}