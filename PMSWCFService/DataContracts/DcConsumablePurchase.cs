using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace PMSWCFService.DataContracts
{
    [DataContract]
    public class DcConsumablePurchase : DcModelBase
    {
        [DataMember]
        public string Category { get; set; }//类别
        [DataMember]
        public string ItemName { get; set; }//名称
        [DataMember]
        public string Specification { get; set; }
        [DataMember]
        public string Details { get; set; }//细节
        [DataMember]
        public double Quantity { get; set; }//数量
        [DataMember]
        public string QuantityUnit { get; set; }//单位
        [DataMember]
        public string Grade { get; set; }//ABC级别
        [DataMember]
        public string Remark { get; set; }

        [DataMember]
        public string Supplier { get; set; } //供应商
        [DataMember]
        public double TotalCost { get; set; } //总花费
        [DataMember]
        public string ProcessHistory { get; set; }//已购买，已到货，发货已到

        [DataMember]
        public string LastUpdateTime { get; set; }//最后一次编辑时间
    }
}