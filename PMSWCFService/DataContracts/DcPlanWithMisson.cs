using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace PMSWCFService.DataContracts
{
    [DataContract]
    public class DcPlanWithMisson
    {
        [DataMember]
        public DcPlanVHP Plan { get; set; }
        [DataMember]
        public DcOrder Misson { get; set; }
    }
}