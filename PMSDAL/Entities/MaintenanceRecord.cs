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
        public DateTime CreateTime { get; set; }
        public string State { get; set; }

        public string Device { get; set; }
        public string Part { get; set; }

        public string Persons { get; set; }//负责维护的人员
        public string Content { get; set; }//维护日志

        public string Remark { get; set; }
    }
}
