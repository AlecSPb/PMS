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
        public Analysis(ISpecs currentSpec)
        {
            dict_parameter = currentSpec;
        }
        private ISpecs dict_parameter;
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
            parameters.AddRange(SolveXRFByKeyStr(model.XRF));

            parameters.Add(GetOtherParameter("Density", model.Density));
            parameters.Add(GetOtherParameter("Weight", model.Weight));

            parameters.AddRange(SolveDimension(model.TargetDimension));

            parameters.AddRange(SolvePlateByKeyStr(model.PlateSpec));

            parameters.AddRange(SolveElementByKeyStr(model.GDMS));

            parameters.AddRange(SolveElementByKeyStr(model.VPI));


            return parameters;
        }


        public List<Parameter> GetPropertiesParameters(ECOA model)
        {
            List<Parameter> parameters = new List<Parameter>();
            parameters.Add(GetOtherParameter("Density", model.Density));
            parameters.Add(GetOtherParameter("Weight", model.Weight));

            parameters.AddRange(SolveDimension(model.TargetDimension));
            parameters.AddRange(SolvePlateByKeyStr(model.PlateSpec));

            return parameters;
        }


        #region 分析区域

        /// <summary>
        /// 获取XRF
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public List<Parameter> SolveXRFByKeyStr(string str)
        {
            List<Parameter> parameters = new List<Parameter>();
            var matches = Regex.Matches(str, @"([a-zA-Z \d]+)=([\w.' ]+);");
            foreach (Match item in matches)
            {
                string short_name = item.Groups[1].Value;
                string element_value = item.Groups[2].Value;
                parameters.Add(GetMainElementParameter(short_name, element_value));
            }

            return parameters;
        }

        public Parameter GetOtherParameter(string characteristic, string value)
        {
            var p = new Parameter();
            p.Characteristic = characteristic;
            //获取缩写名称
            p.Type = "Measurement";
            var basicData = dict_parameter.GetShortName(characteristic);
            p.ShortName = basicData.ShortName;
            p.UnitOfMeasure = basicData.UnitOfMeasure;

            p.MeasurementQualifier = basicData.MeasurementQualifier;
            p.MeasurementType = basicData.MeasurementType;
            p.MeasurementValue = value;

            p.Measurements.Add(new Measurement()
            {
                MeasurementType = basicData.MeasurementType,
                MeasurementValue = value,
                UCL = basicData.UCL.ToString(),
                LCL = basicData.LCL.ToString(),
                MDL = basicData.MDL.ToString(),
                CLCalc = basicData.CLCalc
            });
            return p;
        }

        /// <summary>
        /// 元素专用 shortname to longname
        /// </summary>
        /// <param name="shortname"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public Parameter GetElementParameter(string shortname, string value)
        {
            var p = new Parameter();
            p.Type = "Measurement";
            //获取元素完整名称
            string fullname = dict_element.GetFullName(shortname);
            p.Characteristic = fullname;
            //获取缩写名称
            var basicData = dict_parameter.GetShortName(fullname);

            p.ShortName = basicData.ShortName;
            p.UnitOfMeasure = basicData.UnitOfMeasure;

            p.MeasurementQualifier = basicData.MeasurementQualifier;
            p.MeasurementType = basicData.MeasurementType;
            p.MeasurementValue = value;

            p.Measurements.Add(new Measurement()
            {
                MeasurementType = basicData.MeasurementType,
                MeasurementValue = value,
                UCL = basicData.UCL.ToString(),
                LCL = basicData.LCL.ToString(),
                MDL = basicData.MDL.ToString(),
                CLCalc = basicData.CLCalc
            });
            return p;
        }

        /// <summary>
        /// 生成原材料元素
        /// </summary>
        /// <param name="shortname"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public Parameter GetMainElementParameter(string shortname, string value)
        {
            var p = new Parameter();
            p.Type = "Measurement";
            //获取元素完整名称
            string fullname = dict_element.GetFullName(shortname);
            p.Characteristic = fullname;
            //获取缩写名称
            var basicData = dict_parameter.GetShortName(fullname);
            p.ShortName = basicData.ShortName;
            p.UnitOfMeasure = basicData.UnitOfMeasure;

            p.MeasurementQualifier = basicData.MeasurementQualifier;
            p.MeasurementType = basicData.MeasurementType;
            p.MeasurementValue = value;

            p.Measurements.Add(new Measurement()
            {
                MeasurementType = basicData.MeasurementType,
                MeasurementValue = value,
                UCL = basicData.UCL.ToString(),
                LCL = basicData.LCL.ToString(),
                MDL = basicData.MDL.ToString(),
                CLCalc = basicData.CLCalc
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
                parameters.Add(GetElementParameter(short_name, element_value));
            }
            return parameters;
        }
        /// <summary>
        /// 获取尺寸
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public List<Parameter> SolveDimension(string str)
        {
            List<Parameter> parameters = new List<Parameter>();
            var matches = Regex.Matches(str, @"[0-9]+([.]{1}[0-9]+){0,1}");
            string od_value = matches[0].Value;
            string th_value = matches[1].Value;
            if (dict_parameter.SpecName == "IntelSpecs")
            {
                parameters.Add(GetOtherParameter("Target Blank OD", od_value));
                parameters.Add(GetOtherParameter("Target Blank Thickness", th_value));
            }
            else
            {
                parameters.Add(GetOtherParameter("Target Diameter", od_value));
                parameters.Add(GetOtherParameter("Target Thickness", th_value));
            }
            return parameters;
        }

        /// <summary>
        /// 获取背板参数
        /// </summary>
        /// <param name="str"></param>
        /// <param name="unit"></param>
        /// <returns></returns>
        public List<Parameter> SolvePlateByKeyStr(string str)
        {
            List<Parameter> parameters = new List<Parameter>();
            var matches = Regex.Matches(str, @"([a-zA-Z \d]+)=([\w.' ]+);");
            foreach (Match item in matches)
            {
                string element_name = item.Groups[1].Value;
                string element_value = item.Groups[2].Value;
                parameters.Add(GetOtherParameter(element_name, element_value));
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
        public List<Parameter> SolveElementByKeyStr(string str)
        {
            List<Parameter> parameters = new List<Parameter>();
            var matches = Regex.Matches(str, @"([a-zA-Z \d]+)=([\w.' ]+);");
            foreach (Match item in matches)
            {
                string short_name = item.Groups[1].Value;
                string element_value = item.Groups[2].Value;
                parameters.Add(GetElementParameter(short_name, element_value));

            }

            return parameters;
        }
        #endregion

    }
}
