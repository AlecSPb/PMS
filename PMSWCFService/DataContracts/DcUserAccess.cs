using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace PMSWCFService.DataContracts
{
    [DataContract]
    public class DcUserAccess
    {
        [DataMember]
        public Guid ID { get; set; }
        [DataMember]
        public string AccessName { get; set; }
        [DataMember]
        public string AccessCode { get; set; }
        [DataMember]
        public string State { get; set; }
        [DataMember]
        public string ExtraInformation { get; set; }
    }
}