using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace PMSWCFService.DataContracts
{
    [DataContract]
    public class DcMaterialOrder
    {
        [DataMember]
        public Guid ID { get; set; }
        [DataMember]
        public DateTime CreateTime { get; set; }
        [DataMember]
        public string Creator { get; set; }
        [DataMember]
        public string State { get; set; }

        [DataMember]
        public string OrderPO { get; set; }
        [DataMember]
        public string Supplier { get; set; }
        [DataMember]
        public string SupplierAbbr { get; set; }
        [DataMember]
        public string SupplierReceiver { get; set; }
        [DataMember]
        public string SupplierEmail { get; set; }
        [DataMember]
        public string SupplierAddress { get; set; }
        [DataMember]
        public string Remark { get; set; }
        [DataMember]
        public double ShipFee { get; set; }
        [DataMember]
        public string Priority { get; set; }
        [DataMember]
        public DateTime FinishTime { get; set; }

    }
}
