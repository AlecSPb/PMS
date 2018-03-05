using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace PMSWCFService.DataContracts
{
    [DataContract]
    public class ToDo
    {
        [DataMember]
        public Guid ID { get; set; }
        [DataMember]
        public DateTime CreateTime { get; set; }
        [DataMember]
        public string Creator { get; set; }
        [DataMember]
        public string Status { get; set; }

        [DataMember]
        public string Title { get; set; }
        [DataMember]
        public string Content { get; set; }
        [DataMember]
        public int Priority { get; set; }
        [DataMember]
        public DateTime DeadLine { get; set; }
        [DataMember]
        public DateTime PersonInCharge { get; set; }

        [DataMember]
        public DateTime FinishTime { get; set; }
        [DataMember]
        public string Remark { get; set; }
    }
}
