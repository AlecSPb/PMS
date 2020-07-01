using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace PMSWCFService.DataContracts
{
    /// <summary>
    /// 暂时不用
    /// </summary>
    [DataContract]
    public class DcDeliveryItemTCB
    {
        [DataMember]
        public Guid ID { get; set; }
        [DataMember]
        public DateTime CreateTime { get; set; }
        [DataMember]
        public string Creator { get; set; }
        [DataMember]
        public string ProductType { get; set; }//裸靶 or Bonding or其他
        [DataMember]
        public string ProductID { get; set; }
        [DataMember]
        public string Composition { get; set; }
        [DataMember]
        public string Abbr { get; set; }
        [DataMember]
        public string Customer { get; set; }
        [DataMember]
        public string PO { get; set; }
        [DataMember]
        public string Weight { get; set; }
        [DataMember]
        public string DetailRecord { get; set; }//复杂的信息写在这里
        [DataMember]
        public string Dimension { get; set; }
        [DataMember]
        public string DimensionActual { get; set; }
        [DataMember]
        public string Defects { get; set; }
        [DataMember]
        public string Remark { get; set; }


        [DataMember]
        public Guid DeliveryID { get; set; }
        [DataMember]
        public string BondingPO { get; set; }
        [DataMember]
        public string TrackingHistory { get; set; }
        [DataMember]
        public string TCBRemark { get; set; }
        [DataMember]
        public string State { get; set; }
    }
}