using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMSAnalysis.Models
{
    public class AnalysisModel
    {
        public DateTime PlanDate { get; set; }
        public PlanResult A { get; set; }
        public PlanResult B { get; set; }
        public PlanResult C { get; set; }
        public PlanResult D { get; set; }
        public PlanResult E { get; set; }
        public PlanResult F { get; set; }
        public PlanResult G { get; set; }
    }
}
