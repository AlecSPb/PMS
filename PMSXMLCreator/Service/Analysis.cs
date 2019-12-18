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

            parameters.AddRange(GetProductNameComposition(model.ProductName));

            parameters.Add(GetParameter("Density", model.Density, ParameterUnit.Density, ucl: "4.4", lcl: "4.3"));
            parameters.Add(GetParameter("Weight", model.Weight, ParameterUnit.Weight, ucl: "5290", lcl: "5030"));

            parameters.AddRange(GetDimension(model.TargetDimension));

            parameters.AddRange(GetParametersByKeyStr(model.PlateSpec));

            parameters.AddRange(GetParametersByKeyStr(model.GDMS));

            parameters.AddRange(GetParametersByKeyStr(model.VPI));


            return parameters;
        }


        public List<Parameter> GetPropertiesParameters(ECOA model)
        {
            List<Parameter> parameters = new List<Parameter>();
            parameters.Add(GetParameter("Density", model.Density, ParameterUnit.Density, ucl: "4.4", lcl: "4.3"));
            parameters.Add(GetParameter("Weight", model.Weight, ParameterUnit.Weight, ucl: "5290", lcl: "5030"));

            parameters.AddRange(GetDimension(model.TargetDimension));

            parameters.AddRange(GetParametersByKeyStr(model.PlateSpec));

            return parameters;
        }


        public Parameter GetParameter(string characteristic, string value, string unit = "mm", string type = "Value",
            string ucl = "100", string lcl = "0", string mdl = "0", string clcalc = "Temp")
        {
            var p = new Parameter();
            p.Characteristic = characteristic;
            p.ShortName = dict_parameter.GetShortName(characteristic);
            p.UnitOfMeasure = unit;
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




        #region 分析区域
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
                string element_name = item.Groups[1].Value;
                string element_value = item.Groups[2].Value;
                parameters.Add(GetParameter(dict_element.GetShortName(element_name), element_value, ParameterUnit.Percent));
            }
            return parameters;
        }
        public List<Parameter> GetDimension(string str)
        {
            List<Parameter> parameters = new List<Parameter>();
            var matches = Regex.Matches(str, @"[0-9]+([.]{1}[0-9]+){0,1}");
            string od_value = matches[0].Value;
            string th_value = matches[1].Value;
            parameters.Add(GetParameter("Target Blank OD", od_value));
            parameters.Add(GetParameter("Target Blank Thickness", th_value));
            return parameters;
        }
        public void GetPlate(string str)
        {

        }
        public List<Parameter> GetXRF(string str)
        {
            return null;
        }

        public List<Parameter> GetParametersByKeyStr(string str)
        {
            List<Parameter> parameters = new List<Parameter>();
            var matches = Regex.Matches(str, @"([a-zA-Z ]+)=(\d+);");
            foreach (Match item in matches)
            {
                string element_name = item.Groups[1].Value;
                string element_value = item.Groups[2].Value;
                parameters.Add(GetParameter(dict_element.GetShortName(element_name), element_value, ParameterUnit.PPM));
            }

            return parameters;
        }
        #endregion

    }
}
