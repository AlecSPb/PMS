using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMSXMLCreator.XMLGenerator
{
    /// <summary>
    /// 项目分析类
    /// </summary>
    public class Analysis
    {
        private ParameterDict dict = new ParameterDict();
        public List<Parameter> GetParamenters(ECOA model)
        {
            List<Parameter> parameters = new List<Parameter>();

            parameters.Add(GetParameter("Density", model.Density, ParameterUnit.Density, "4.4", "4.3"));
            parameters.Add(GetParameter("Weight", model.Weight, ParameterUnit.Weight, "5290", "5030"));


            return parameters;
        }

        public Parameter GetParameter(string characteristic, string value, string unit = "mm", string ucl = "100", string lcl = "0", string mdl = "0", string clcalc = "Temp")
        {
            var p = new Parameter();
            p.Characteristic = characteristic;
            p.ShortName = dict.GetShortName(characteristic);
            p.UnitOfMeasure = unit;
            p.Measurements.Add(new Measurement() { MeasurementType = "Value", MeasurementValue = value, UCL = ucl, LCL = lcl, MDL = mdl, CLCalc = clcalc });
            return p;
        }

    }
}
