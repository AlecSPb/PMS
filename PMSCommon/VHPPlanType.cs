using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMSCommon
{
    /// <summary>
    /// 热压计划类型
    /// 指示热压目的
    /// </summary>
    public enum VHPPlanType
    {

        回收,
        加工,
        外协,
        代工,
        烤料,
        试机,
        发货,
        其他
    }
}
