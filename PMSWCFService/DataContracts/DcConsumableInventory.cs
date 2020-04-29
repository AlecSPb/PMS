using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace PMSWCFService.DataContracts
{
    [DataContract]
    public class DcConsumableInventory:DcModelBase
    {
        [DataMember]
        public string Category { get; set; }//类别
        [DataMember]
        public string ItemName { get; set; }//名称
        [DataMember]
        public string Specification { get; set; }//名称
        [DataMember]
        public string Details { get; set; }//细节
        [DataMember]
        public double Quantity { get; set; }//数量
        [DataMember]
        public string QuantityUnit { get; set; }//单位

        [DataMember]
        public string Grade { get; set; }//ABC级别
        [DataMember]
        public string StorePosition { get; set; }//存储位置
        [DataMember]
        public string PersonInCharge { get; set; }//负责人
        [DataMember]
        public double MinWarningQuantity { get; set; }//最小报警量

        [DataMember]
        public string History { get; set; }//存储历史
        [DataMember]
        public string Remark { get; set; }
        [DataMember]
        public string LastUpdateTime { get; set; }//最后一次编辑时间
    }
}