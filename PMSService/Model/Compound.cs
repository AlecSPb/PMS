using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace WcfService.Model
{
    [DataContract]
    public class Compound
    {
        [DataMember]
        public Guid CompoundId { get; set; }
        [DataMember]
        public string CompoundName { get; set; }
        [DataMember]
        public double Density { get; set; }
        [DataMember]
        public double MeltingPoint { get; set; }
        [DataMember]
        public double BoilingPoint { get; set; }
        [DataMember]
        public string SpecialProperty { get; set; }
        [DataMember]
        public DateTime UpdateTime { get; set; }
        [DataMember]
        public string InformationSource { get; set; }
        [DataMember]
        public string Remark { get; set; }
    }
}