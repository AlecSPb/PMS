using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMSDAL
{
    public class MaintenanceRecord
    {
        public Guid ID { get; set; }
        public string Creator { get; set; }
        public string CreateTime { get; ; set; }
        public string State { get; set; }

        public Guid PlanID { get; set; }
        public string MaintenancePersons { get; set; }//负责维护的人员
        public string MaintenanceContent { get; set; }//维护日志
    }
}
