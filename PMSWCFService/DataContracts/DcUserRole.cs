using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace PMSWCFService.DataContracts
{
    [DataContract]
    public class DcUserRole
    {
        [DataMember]
        public Guid ID { get; set; }
        [DataMember]
        public string GroupName { get; set; }
        [DataMember]
        public string ExtraInformation { get; set; }
        [DataMember]
        public string State { get; set; }
        [DataMember]
        public DateTime CreateTime { get; set; }
    }
}