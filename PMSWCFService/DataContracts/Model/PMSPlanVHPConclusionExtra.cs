using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PMSDAL;

namespace PMSWCFService.DataContracts
{
    public class PMSPlanVHPConclusionExtra
    {
        public PlanVHPConclusion Conclusion { get; set; }
        public PMSPlanVHP PlanVHP { get; set; }
        public PMSOrder Order { get; set; }
    }
}