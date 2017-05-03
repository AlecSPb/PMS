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
    public class PMSPlanExtraModel
    {
        public PMSPlanVHP Plan { get; set; }
        public PMSOrder Misson { get; set; }
    }
}