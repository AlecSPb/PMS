using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMSCommon
{
    /// <summary>
    /// 用于三杰库存原材料追踪
    /// </summary>
    public enum RawMaterialSheetState
    {
        作废,
        在库,
        耗尽,
        取消
    }
}
