using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace PMSWCFService.DataContracts
{
    [DataContract]
    public class DcFeedBack
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
        public string ProductID { get; set; }
        [DataMember]
        public string ProductType { get; set; }
        [DataMember]
        public string Composition { get; set; }
        [DataMember]
        public string Customer { get; set; }
        [DataMember]
        public string FeedbackReason { get; set; }
        [DataMember]
        public string ProcessWay { get; set; }
        [DataMember]
        public string Remark { get; set; }
    }
}
