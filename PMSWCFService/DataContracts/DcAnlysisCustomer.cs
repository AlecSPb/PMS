using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace PMSWCFService.DataContracts
{
    [DataContract]
    public class DcAnlysisCustomer
    {
        [DataMember]
        public string CustomerName { get; set; }
        [DataMember]
        public int OrderCount { get; set; }
        [DataMember]
        public double TargetQuantity { get; set; }
    }
}