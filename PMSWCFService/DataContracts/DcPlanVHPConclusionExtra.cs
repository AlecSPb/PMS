using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PMSWCFService.DataContracts
{
    public class DcPlanVHPConclusionExtra
    {
        public DcPlanVHPConclusion Conclusion { get; set; }
        public DcPlanVHP PlanVHP { get; set; }
        public DcOrder Order { get; set; }
    }
}