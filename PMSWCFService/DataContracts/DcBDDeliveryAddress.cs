using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace PMSWCFService.DataContracts
{
    [DataContract]
    public class DcBDDeliveryAddress
    {
        [DataMember]
        public Guid ID { get; set; }
        [DataMember]
        public string Receiver { get; set; }
        [DataMember]
        public string Company { get; set; }
        [DataMember]
        public string Country { get; set; }
        [DataMember]
        public string Tax { get; set; }
        [DataMember]
        public string Phone { get; set; }
        [DataMember]
        public string PostCode { get; set; }
        [DataMember]
        public string Email { get; set; }
        [DataMember]
        public string CellPhone { get; set; }
        [DataMember]
        public string Address { get; set; }
        [DataMember]
        public string State { get; set; }
    }
}
