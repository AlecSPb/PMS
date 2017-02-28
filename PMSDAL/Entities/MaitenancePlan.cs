using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMSDAL
{
    public class MaitenancePlan
    {
        public Guid ID { get; set; }
        public string Creator { get; set; }
        public string CreateTime { get; ; set; }
        public string State { get; set; }//Run,Stop,Paused

        public string DeviceCode { get; set; }
        public string PlanItem { get; set; }

        public int IntervalCount { get; set; }
        public int CurrentCount { get; set; }

        public string Remark { get; set; }
    }
}
