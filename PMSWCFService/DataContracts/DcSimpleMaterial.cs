using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace PMSWCFService.DataContracts
{
    [DataContract]
    public class DcSimpleMaterial:DcModelBase
    {
        [DataMember]
        public string ElementName { get; set; }
        [DataMember]
        public double UnitPrice { get; set; }
        [DataMember]
        public string Remark { get; set; }
    }
}