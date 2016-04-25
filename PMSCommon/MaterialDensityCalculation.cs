using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UsefulPackage
{
    public class MaterialDensityCalculation
    {
        public double WeightingDensity(IEnumerable<MaterialDensityCalculationItem> items)
        {
            //错误输入验证代码后续

            double[] tmpValue = new double[items.Count()];
            double sum = items.Sum(i => i.MoleWeight * i.At);
            double weightingDensity = items.Sum(i => i.At * i.MoleWeight / sum * i.Density);

            return weightingDensity;
        }
    }
}
