using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PMSCommon
{
    /// <summary>
    /// 订单状态类型
    /// deleted不显示
    /// paused是暂停但是显示
    /// umcompleted是未完成
    /// UnDetermined是未确定，所以暂不在除了管理端的其他地方显示
    /// completed是完成
    /// </summary>
    public enum OrderState
    {
        Deleted = 0,
        Paused = 1,
        UnCompleted=2,
        UnChecked = 3,
        Completed =4
    }
}