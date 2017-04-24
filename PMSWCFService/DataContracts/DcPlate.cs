using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace PMSWCFService.DataContracts
{
    [DataContract]
    public class DcPlate
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
        public string PlateLot { get; set; }
        [DataMember]
        public string Dimension { get; set; }
        [DataMember]
        public string Supplier { get; set; }
        [DataMember]
        public string UseCount { get; set; }
        [DataMember]
        public string Hardness { get; set; }
        [DataMember]
        public string LastWeldMaterial { get; set; }
        [DataMember]
        public string Weight { get; set; }
        [DataMember]
        public string Appearance { get; set; }
        [DataMember]
        public string Defects { get; set; }
        [DataMember]
        public string Remark { get; set; }
    }
}
