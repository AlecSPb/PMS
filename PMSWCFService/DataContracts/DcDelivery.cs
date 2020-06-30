using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace PMSWCFService.DataContracts
{
    //发货单
    [DataContract]
    public class DcDelivery
    {
        [DataMember]
        public Guid ID { get; set; }
        [DataMember]
        public DateTime CreateTime { get; set; }
        [DataMember]
        public string Creator { get; set; }
        [DataMember]
        public DateTime FinishTime { get; set; }
        [DataMember]
        public string DeliveryName { get; set; }
        [DataMember]
        public string InvoiceNumber { get; set; }
        [DataMember]
        public string DeliveryExpress { get; set; }
        [DataMember]
        public string DeliveryNumber { get; set; }
        [DataMember]
        public string Country { get; set; }
        [DataMember]
        public string Address { get; set; }
        [DataMember]
        public DateTime ShipTime { get; set; }
        [DataMember]
        public string Remark { get; set; }
        [DataMember]
        public string PackageType { get; set; }//木箱 纸箱 塑料箱
        [DataMember]
        public string PackageInformation { get; set; }//包装重量等细节信息
        [DataMember]
        public string LastCheckIDCollection { get; set; }//最后一次检查录入的ID信息
        [DataMember]
        public string PackageWeight { get; set; }//包装重量
        [DataMember]
        public bool IsCustomerSigned { get; set; }
        [DataMember]
        public DateTime CustomerSignedDate { get; set; }//签收时间
        [DataMember]
        public string CustomerSignedDetails { get; set; }//签收细节
        [DataMember]
        public string Receiver { get; set; }//接收者
        [DataMember]
        public string State { get; set; }//取消，未审核，审核通过，已发货

        [DataMember]
        public DateTime LastUpdateTime { get; set; }


    }
}
