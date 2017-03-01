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
        public string CreateTime { get;  set; }
        [DataMember]
        public string State { get; set; }
        [DataMember]
        public Guid PlanID { get; set; }
        [DataMember]
        public string MaintenancePersons { get; set; }//负责维护的人员
        [DataMember]
        public string MaintenanceContent { get; set; }//维护日志
    }
}
