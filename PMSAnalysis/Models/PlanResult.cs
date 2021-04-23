using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMSAnalysis.Models
{
    /// <summary>
    /// 热压结果状态
    /// </summary>
    public enum PlanResult
    {
        Empty,
        W1,
        W2_Success,
        W2_Fail
    }
}
