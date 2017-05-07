using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace PMSWCFService.DataContracts
{
    [DataContract]
    public class DcCheckList
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
        public DateTime UpdateTime { get; set; }
        [DataMember]
        public string Updator { get; set; }
        [DataMember]
        public string Title { get; set; }
        [DataMember]
        public string Content { get; set; }
    }
}
