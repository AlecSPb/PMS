using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace PMSWCFService.DataContracts
{
    [DataContract]
    public class DcStatistic
    {
        [DataMember]
        public string Key { get; set; }
        [DataMember]
        public double Value { get; set; }
    }
}