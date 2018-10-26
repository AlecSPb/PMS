using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace PMSWCFService.DataContracts
{
    [DataContract]
    public class DcFailure
    {
        [DataMember]
        public Guid ID { get; set; }
        [DataMember]
        public DateTime CreateTime { get; set; }
        [DataMember]
        public string Creator { get; set; }

        [DataMember]
        public string State { get; set; }

        [DataMember]
        public string ProductID { get; set; }

        [DataMember]
        public string Composition { get; set; }
        //所有的其他信息全部写入Details，用;隔开
        [DataMember]
        public string Details { get; set; }

        [DataMember]
        public string Stage { get; set; }

        [DataMember]
        public string Problem { get; set; }
        [DataMember]
        public string Process { get; set; }

        [DataMember]
        public string Remark { get; set; }
    }
}