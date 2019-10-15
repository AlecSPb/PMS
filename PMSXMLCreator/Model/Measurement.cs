using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMSXMLCreator
{
    /// <summary>
    /// COA Measurement for Parameter
    /// </summary>
    public class Measurement
    {
        public string MeasurementType { get; set; }
        public string MeasurementValue { get; set; }
        public string UCL { get; set; }
        public string LCL { get; set; }
        public string MDL { get; set; }
        //Temp, Eng, Short-run, Analytical, Mean-sigma, Percentile, Curve-fitted
        public string CLCalc { get; set; }
    }
}
