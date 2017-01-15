using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace PMSWCFService.DataContracts
{
    [DataContract]
    public class DcCustomer
    {
        [DataMember]
        public Guid ID { get; set; }
        [DataMember]
        public string CustomerName { get; set; }
        [DataMember]
        public string Address { get; set; }
        [DataMember]
        public string ContactPerson { get; set; }
        [DataMember]
        public string Phone1 { get; set; }
        [DataMember]
        public string Phone2 { get; set; }
        [DataMember]
        public string Fax { get; set; }
        [DataMember]
        public string Email { get; set; }
        [DataMember]
        public string Memo { get; set; }
    }
}
