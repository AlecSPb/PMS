using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMSQuotation.Services
{
    public class CalculationResult
    {
        public CalculationResult()
        {
            TargetFee = ExtraFee = TaxFee = 0.0;
        }
        public double TargetFee { get; set; }
        public double ExtraFee { get; set; }
        public double TaxFee { get; set; }

    }
}
