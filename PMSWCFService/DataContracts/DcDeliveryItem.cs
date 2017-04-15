using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace PMSWCFService.DataContracts
{
    /// <summary>
    /// 送货单项目
    /// 不涉及过多产品具体信息，在所有产品和样品之间通用
    /// </summary>
    [DataContract]
    public class DcDeliveryItem
    {
        [DataMember]
        public Guid ID { get; set; }
        [DataMember]
        public DateTime CreateTime { get; set; }
        [DataMember]
        public string Creator { get; set; }
        [DataMember]
        public string ProductType { get; set; }//产品 or 样品 or其他
        [DataMember]
        public string ProductID { get; set; }//根据这个来查询具体信息
        [DataMember]
        public string Composition { get; set; }
        [DataMember]
        public string Abbr { get; set; }
        [DataMember]
        public string Customer { get; set; }
        [DataMember]
        public string PO { get; set; }
        [DataMember]
        public string DetailRecord { get; set; }//复杂的信息写在这里
        [DataMember]
        public string Weight { get; set; }
        [DataMember]
        public string Position { get; set; }//入库库房编号，位置编号，unknown，成品库房 产品架A
        [DataMember]
        public string Remark { get; set; }
        [DataMember]
        public string State { get; set; }
        [DataMember]
        public Guid DeliveryID { get; set; }
    }
}
