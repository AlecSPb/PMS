using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace PMSWCFService.DataContracts
{
    [DataContract]
    public class DcMaintenanceRecord
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
        public string Device { get; set; }
        [DataMember]
        public string Part { get; set; }

        [DataMember]
        public string Persons { get; set; }//负责维护的人员
        [DataMember]
        public string Content { get; set; }//维护日志

        [DataMember]
        public string Remark { get; set; }
    }
}
