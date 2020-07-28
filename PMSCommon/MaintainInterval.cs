using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMSCommon
{
    /// <summary>
    /// 维护保养的间隔时间
    /// </summary>
    public enum MaintainInterval
    {
        EveryDay每天,
        EveryWeek每周,
        Every2Week每两周,
        EveryMonth每月,
        Every2Month每两月,
        EverySeason每季度,
        EveryYear每年
    }
}
