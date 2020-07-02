using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMSXMLCreator.Service
{
    /// <summary>
    /// 基本的Intel规格模型
    /// </summary>
    public class SpecModel
    {
        public SpecModel()
        {
            MeasurementQualifier = "";
            MeasurementType = "Value";
            CLCalc = "Temp";
            MDL = 0;
            LCL = 0;
            UCL = 100;
            UnitOfMeasure = ParameterUnit.MM;
        }
        public string MeasurementType { get; set; }
        public string MeasurementQualifier { get; set; }
        public string ShortName { get; set; }
        public double UCL { get; set; }
        public double LCL { get; set; }
        public double MDL { get; set; }
        public string CLCalc { get; set; }
        public string UnitOfMeasure { get; set; }

    }
}
