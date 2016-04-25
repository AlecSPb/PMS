using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UsefulPackage
{
    /// <summary>
    ///靶材密度计算法-计算项
    /// </summary>
    public class TargetDensityCalculationItem
    {
        public double TargetWeight { get; set; }
        public double[] OD { get; set; }
        public double[] H { get; set; }
        public double TheoryDensity { get; set; }
        public double PaperWeight { get; set; }
        public double PaperThickness { get; set; }

        public TargetDensityCalculationItem()
        {
            TheoryDensity = 5;
            PaperWeight = 0;
            PaperThickness=0;
        }
    }
}
