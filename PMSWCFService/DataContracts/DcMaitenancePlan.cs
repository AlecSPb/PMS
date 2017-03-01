using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace PMSWCFService.DataContracts
{
    [DataContract]
    public class DcMaintenancePlan
    {
        [DataMember]
        public Guid ID { get; set; }
        [DataMember]
        public string Creator { get; set; }
        [DataMember]
        public string CreateTime { get; set; }
        [DataMember]
        public string State { get; set; }//Run,Stop,Paused
        [DataMember]
        public string DeviceCode { get; set; }
        [DataMember]
        public string PlanItem { get; set; }
        [DataMember]
        public int IntervalCount { get; set; }
        [DataMember]
        public int CurrentCount { get; set; }
        [DataMember]
        public string Remark { get; set; }
    }
}
