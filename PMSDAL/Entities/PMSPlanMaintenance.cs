using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMSDAL.Entities
{
    /// <summary>
    /// 一个计划对应一个设备，比如旋片泵，扩散泵
    /// </summary>
    public class PMSPlanMaintenance
    {
        public Guid ID { get; set; }
        public DateTime CreateTime { get; set; }
        public string Creator { get; set; }


        public string VHPDeviceCode { get; set; }
        public string DeviceName { get; set; }
        public DateTime StartTime { get; set; }
        public int CounterInterval { get; set; }//计数周期
        public int CurrentCounter { get; set; }//当前计数，如果能被计数周期整除，弹出提醒

        public string State { get; set; }
    }
}
