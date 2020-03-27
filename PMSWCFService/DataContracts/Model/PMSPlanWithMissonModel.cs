using PMSDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PMSWCFService.DataContracts
{
    /// <summary>
    /// 辅助类
    /// </summary>
    [Obsolete("请使用PMSPlanExtraModel类代替")]
    public class PMSPlanWithMissonModel
    {
        public PMSPlanVHP Plan { get; set; }
        public PMSOrder Misson { get; set; }
    }
}