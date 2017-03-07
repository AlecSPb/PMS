using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace PMSWCFService.DataContracts
{
    [DataContract]
    public class BDSieve
    {
        [DataMember]
        public Guid ID { get; set; }
        [DataMember]
        public string SieveName { get; set; }
        [DataMember]
        public string Mesh { get; set; }
        [DataMember]
        public DateTime StartTime { get; set; }
        [DataMember]
        public string Manufacuture { get; set; }
        [DataMember]
        public int EstimateUsedCount { get; set; }
        [DataMember]
        public int CurrentCount { get; set; }
        [DataMember]
        public string State { get; set; }
        [DataMember]
        public string Creator { get; set; }
        [DataMember]
        public DateTime CreateTime { get; set; }
    }
}
