using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace PMSWCFService.DataContracts
{
    [DataContract]
    public class DcRecordTest
    {
        [DataMember]
        public Guid ID { get; set; }
        [DataMember]
        public DateTime CreateTime { get; set; }
        [DataMember]
        public string Creator { get; set; }
        [DataMember]
        public string State { get; set; }//未审核，审核，作废
        [DataMember]
        public string PMINumber { get; set; }
        [DataMember]
        public string TestType { get; set; }
        [DataMember]
        public string ProductID { get; set; }
        [DataMember]
        public string Composition { get; set; }
        [DataMember]
        public string CompositionAbbr { get; set; }
        [DataMember]
        public string PO { get; set; }
        [DataMember]
        public string Customer { get; set; }
        [DataMember]
        public string Dimension { get; set; }
        [DataMember]
        public string Density { get; set; }
        [DataMember]
        public string Weight { get; set; }
        [DataMember]
        public string Resistance { get; set; }
        [DataMember]
        public string CompositionXRF { get; set; }
        [DataMember]
        public string DimensionActual { get; set; }
        [DataMember]
        public string Defects { get; set; }
        [DataMember]
        public string Remark { get; set; }
        [DataMember]
        public string Sample { get; set; }
        [DataMember]
        public DateTime OrderDate { get; set; }
        [DataMember]
        public string FollowUps { get; set; }
        [DataMember]
        public string Roughness { get; set; }

        [DataMember]
        public string Warping { get; set; }


    }
}
