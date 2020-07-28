using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMSCommon
{
    /// <summary>
    /// 维护保养的类型
    /// </summary>
    public enum MaintainType
    {
        Routine例行保养,
        Level_1一级保养,
        Level_2二级保养,
        Quarterly季节保养,
        Annually年度保养,
        EmergencyFix突发故障维修
    }
}
