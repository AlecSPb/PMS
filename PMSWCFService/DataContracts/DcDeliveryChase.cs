using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace PMSWCFService.DataContracts
{
    /// <summary>
    /// 货物交付追踪-
    /// </summary>
    [DataContract]
    public class DcDeliveryChase : DcModelBase
    {
        [DataMember]
        public string ProductID { get; set; }
        [DataMember]
        public string Composition { get; set; }
        [DataMember]
        public string PO { get; set; }
        [DataMember]
        public string PMINumber { get; set; }
        [DataMember]
        public string Customer { get; set; }
        [DataMember]
        public string Dimension { get; set; }
        [DataMember]
        public double Weight { get; set; }
        [DataMember]
        public string TraceInformation { get; set; }
        [DataMember]
        public string Remark { get; set; }

    }
}
