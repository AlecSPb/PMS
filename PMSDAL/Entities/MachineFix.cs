using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMSDAL
{
    /// <summary>
    /// 记录机器故障
    /// </summary>
    public class MachineFix : ModelBase
    {
        public string FixType { get; set; }//保养还是维修
        public string DeviceName { get; set; }//设备
        public string PartName { get; set; }//部件
        public string EventDescription { get; set; }//现象，原因
        public string FixMeasure { get; set; }//处理方案和结果
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Remark { get; set; }
    }
}
