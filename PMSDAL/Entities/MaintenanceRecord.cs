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

        public string VHPMachineCode { get; set; }//计划
        public string PlanItem { get; set; }//计划
        public string PlanType { get; set; }//计划类型，一级保养，二级保养，三级保养,
        public string PlanInterval { get; set; }//计划周期
        public string Content { get; set; }//计划内容

        public string Persons { get; set; }//负责维护的人员
        public string Log { get; set; }//维护日志

        public string Remark { get; set; }
    }
}
