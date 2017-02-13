using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace PMSWCFService.DataContracts
{
    [DataContract]
    public class DcBDCompound
    {
        [DataMember]
        public Guid ID { get; set; }
        [DataMember]
        public string MaterialName { get; set; }
        [DataMember]
        public double Density { get; set; }
        [DataMember]
        public string MeltingPoint { get; set; }
        [DataMember]
        public string BoilingPoint { get; set; }
        [DataMember]
        public string SpecialProperty { get; set; }
        [DataMember]
        public string InformationSource { get; set; }
        [DataMember]
        public string Creator { get; set; }
        [DataMember]
        public DateTime CreateTime { get; set; }
        [DataMember]
        public string State { get; set; }
    }
}
