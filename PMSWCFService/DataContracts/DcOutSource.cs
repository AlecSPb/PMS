using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace PMSWCFService.DataContracts
{
    [DataContract]
    public class DcOutSource
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
        public string OrderLot { get; set; }
        [DataMember]
        public string OrderType { get; set; }
        [DataMember]
        public string OrderName { get; set; }
        [DataMember]
        public string Supplier { get; set; }
        [DataMember]
        public string Dimension { get; set; }
        [DataMember]
        public double Quantity { get; set; }
        [DataMember]
        public string QuantityUnit { get; set; }
        [DataMember]
        public double Cost { get; set; }
        [DataMember]
        public string Remark { get; set; }

    }
}
