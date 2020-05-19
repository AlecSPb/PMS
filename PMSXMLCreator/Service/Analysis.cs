using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace PMSXMLCreator.Service
{
    /// <summary>
    /// 项目分析类
    /// </summary>
    public class Analysis
    {
        private ParameterDict dict_parameter = new ParameterDict();
        private ElementFullNameDict dict_element = new ElementFullNameDict();
        /// <summary>
        /// 获取所有ECOA模型参数
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public List<Parameter> GetAllECOAParamenters(ECOA model)
        {
            List<Parameter> parameters = new List<Parameter>();

            //parameters.AddRange(GetProductNameComposition(model.ProductName));
            parameters.AddRange(GetXRFByKeyStr(model.XRF));

            parameters.Add(GetOtherParameter("Density", model.Density, ParameterUnit.Density, ucl: "4.4", lcl: "4.3"));
            parameters.Add(GetOtherParameter("Weight", model.Weight, ParameterUnit.Density, ucl: "5290", lcl: "5030"));

            parameters.AddRange(GetDimension(model.TargetDimension));

            parameters.AddRange(GetPlateByKeyStr(model.PlateSpec, ParameterUnit.MM));

            parameters.AddRange(GetElementByKeyStr(model.GDMS, ParameterUnit.PPM));

            parameters.AddRange(GetElementByKeyStr(model.VPI, ParameterUnit.PPM));


            return parameters;
        }


        public List<Parameter> GetPropertiesParameters(ECOA model)
        {
            List<Parameter> parameters = new List<Parameter>();
            parameters.Add(GetOtherParameter("Density", model.Density, ParameterUnit.Density, ucl: "0", lcl: "0"));
            parameters.Add(GetOtherParameter("Weight", model.Weight, ParameterUnit.Weight, ucl: "0", lcl: "0"));

            parameters.AddRange(GetDimension(model.TargetDimension));
            parameters.AddRange(GetPlateByKeyStr(model.PlateSpec, "mm"));

            return parameters;
        }


        #region 分析区域

        public Parameter GetOtherParameter(string characteristic, string value, string unit = "mm",
                string type = "Value", string qualifier = "", string ucl = "100", string lcl = "0", string mdl = "0", string clcalc = "Temp")
        {
            var p = new Parameter();
            p.Characteristic = characteristic;
            //获取缩写名称
            p.ShortName = dict_parameter.GetShortName(characteristic);
            p.UnitOfMeasure = unit;

            p.MeasurementQualifier = qualifier;
            p.MeasurementType = type;
            p.MeasurementValue = value;

            p.Measurements.Add(new Measurement()
            {
                MeasurementType = type,
                MeasurementValue = value,
                UCL = ucl,
                LCL = lcl,
                MDL = mdl,
                CLCalc = clcalc
            });
            return p;
        }

        /// <summary>
        /// 元素专用 shortname to longname
        /// </summary>
        /// <param name="shortname"></param>
        /// <param name="value"></param>
        /// <param name="unit"></param>
        /// <param name="type"></param>
        /// <param name="ucl"></param>
        /// <param name="lcl"></param>
        /// <param name="mdl"></param>
        /// <param name="clcalc"></param>
        /// <returns></returns>
        public Parameter GetElementParameter(string shortname, string value, string unit = "mm",
        string type = "Value", string qualifier = "", string ucl = "100", string lcl = "0", string mdl = "0", string clcalc = "Temp")
        {
            var p = new Parameter();
            //获取元素完整名称
            string fullname = dict_element.GetFullName(shortname);
            p.Characteristic = fullname;
            //获取缩写名称
            p.ShortName = dict_parameter.GetShortName(fullname);
            p.UnitOfMeasure = unit;

            p.MeasurementQualifier = qualifier;
            p.MeasurementType = type;
            p.MeasurementValue = value;

            p.Measurements.Add(new Measurement()
            {
                MeasurementType = type,
                MeasurementValue = value,
                UCL = ucl,
                LCL = lcl,
                MDL = mdl,
                CLCalc = clcalc
            });
            return p;
        }
        /// <summary>
        /// 分析成分ProductName
        /// </summary>
        /// <param name="str">ProductName字符串</param>
        /// <returns></returns>
        public List<Parameter> GetProductNameComposition(string str)
        {
            List<Parameter> parameters = new List<Parameter>();
            var matches = Regex.Matches(str, @"([a-zA-Z]+)([0-9]+([.]{1}[0-9]+){0,1})");
            foreach (Match item in matches)
            {
                string short_name = item.Groups[1].Value;
                string element_value = item.Groups[2].Value;
                parameters.Add(GetElementParameter(short_name, element_value, ParameterUnit.Percent));
            }
            return parameters;
        }
        /// <summary>
        /// 获取尺寸
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public List<Parameter> GetDimension(string str)
        {
            List<Parameter> parameters = new List<Parameter>();
            var matches = Regex.Matches(str, @"[0-9]+([.]{1}[0-9]+){0,1}");
            string od_value = matches[0].Value;
            string th_value = matches[1].Value;
            parameters.Add(GetOtherParameter("Target Blank OD", od_value));
            parameters.Add(GetOtherParameter("Target Blank Thickness", th_value));
            return parameters;
        }

        /// <summary>
        /// 获取背板参数
        /// </summary>
        /// <param name="str"></param>
        /// <param name="unit"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public List<Parameter> GetPlateByKeyStr(string str, string unit = "mm", string type = "value", string ucl = "100", string lcl = "0")
        {
            List<Parameter> parameters = new List<Parameter>();
            var matches = Regex.Matches(str, @"([a-zA-Z \d]+)=([\w.' ]+);");
            foreach (Match item in matches)
            {
                string element_name = item.Groups[1].Value;
                string element_value = item.Groups[2].Value;
                string tempUnit = unit;
                //粗糙度单位单独设置
                if (element_name.Contains("Roughness"))
                {
                    tempUnit = ParameterUnit.UM;
                }
                else
                {
                    tempUnit = unit;
                }
                double ucl_value = 100;
                //if(double.TryParse(element_value,out ucl_value))
                //{
                //    ucl_value =ucl_value + 10;
                //}
                //else
                //{
                //    ucl_value = 100;
                //}
                parameters.Add(GetOtherParameter(element_name, element_value, tempUnit, type, ucl: ucl_value.ToString("F0"), lcl: lcl));
            }

            return parameters;
        }
        /// <summary>
        /// 获取XRF
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public List<Parameter> GetXRFByKeyStr(string str)
        {
            List<Parameter> parameters = new List<Parameter>();
            var matches = Regex.Matches(str, @"([a-zA-Z \d]+)=([\w.' ]+);");
            foreach (Match item in matches)
            {
                string short_name = item.Groups[1].Value;
                string element_value = item.Groups[2].Value;
                parameters.Add(GetElementParameter(short_name, element_value, ParameterUnit.Percent, "value", "at"));
            }

            return parameters;
        }

        /// <summary>
        /// 获取元素参数
        /// </summary>
        /// <param name="str"></param>
        /// <param name="unit"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public List<Parameter> GetElementByKeyStr(string str, string unit = "ppm", string type = "value")
        {
            List<Parameter> parameters = new List<Parameter>();
            var matches = Regex.Matches(str, @"([a-zA-Z \d]+)=([\w.' ]+);");
            foreach (Match item in matches)
            {
                string short_name = item.Groups[1].Value;
                string element_value = item.Groups[2].Value;
                if (short_name == "W")
                {
                    parameters.Add(GetElementParameter(short_name, element_value, ParameterUnit.PPB, type));
                }
                else
                {
                    parameters.Add(GetElementParameter(short_name, element_value, unit, type));
                }
            }

            return parameters;
        }
        #endregion

    }
}
