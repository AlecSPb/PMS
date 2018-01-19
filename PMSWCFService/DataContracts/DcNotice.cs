using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace PMSWCFService.DataContracts
{
    [DataContract]
    public class DcNotice
    {
        [DataMember]
        public Guid ID { get; set; }
        [DataMember]
        public string Content { get; set; }
        [DataMember]
        public string Title { get; set; }
        [DataMember]
        public string Creator { get; set; }
        [DataMember]
        public DateTime CreateTime { get; set; }
    }
}