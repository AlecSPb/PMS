using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace PMSWCFService.DataContracts
{
    [DataContract]
    public class DcPMICounter
    {
        [DataMember]
        public string ItemGroup { get; set; }
        [DataMember]
        public string ItemName { get; set; }
        [DataMember]
        public string ItemSpecification { get; set; }
        [DataMember]
        public string ItemDetails { get; set; }
        [DataMember]
        public double ItemCount { get; set; }
        [DataMember]
        public string Unit { get; set; }
        [DataMember]
        public string ItemHistory { get; set; }
        [DataMember]
        public int RowOrder { get; set; }

        [DataMember]
        public Guid ID { get; set; }
        [DataMember]
        public string Creator { get; set; }
        [DataMember]
        public DateTime CreateTime { get; set; }
        [DataMember]
        public string State { get; set; }
    }
}