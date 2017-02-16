using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace PMSWCFService.DataContracts
{
    [DataContract]
    public class DcBDSupplier
    {
        [DataMember]
        public Guid ID { get; set; }
        [DataMember]
        public string SupplierName { get; set; }
        [DataMember]
        public string Abbr { get; set; }
        [DataMember]
        public string ContactPerson { get; set; }
        [DataMember]
        public string CellPhone { get; set; }
        [DataMember]
        public string Address { get; set; }
        [DataMember]
        public string Email { get; set; }
        [DataMember]
        public string Remark { get; set; }
    }
}