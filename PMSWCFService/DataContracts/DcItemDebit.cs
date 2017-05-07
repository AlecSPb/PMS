using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace PMSWCFService.DataContracts
{
    [DataContract]
    public class DcItemDebit
    {
        [DataMember]
        public Guid ID { get; set; }
        [DataMember]
        public string Creator { get; set; }
        [DataMember]
        public DateTime CreateTime { get; set; }
        [DataMember]
        public string State { get; set; }
        [DataMember]
        public string ItemType { get; set; }//材料还是物品
        [DataMember]
        public string ItemLot { get; set; }//编号
        [DataMember]
        public string ItemName { get; set; }//项目
        [DataMember]
        public string ItemProperty { get; set; }
        [DataMember]
        public double Quantity { get; set; }
        [DataMember]
        public double UnitPrice { get; set; }
        [DataMember]
        public string Creditor { get; set; }
        [DataMember]
        public double Remark { get; set; }
    }
}
