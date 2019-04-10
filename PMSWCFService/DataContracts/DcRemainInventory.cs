using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace PMSWCFService.DataContracts
{
    [DataContract]
    public class DcRemainInventory
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
        public string ProductID { get; set; }
        [DataMember]
        public string Composition { get; set; }
        [DataMember]
        public string Dimension { get; set; }
        [DataMember]
        public string Details { get; set; }
    }
}