using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMSQuotation.Models
{
    public class CostItemAnalysis
    {
        public string AnalysisType { get; set; }
        public double UnitPrice { get; set; }
        public int Quantity { get; set; }
    }
}
