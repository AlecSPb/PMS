using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using PMSClient.SimpleMaterialService;

namespace PMSClient.Components.MaterialOrder
{
    /// <summary>
    /// 原材料订单关于提供原料部分的处理程序
    /// </summary>
    public class SimpleMaterialHelper
    {

        public static string GetMaterialStrFromComposition(string composition, bool useDB = true)
        {
            string s = "";
            try
            {
                if (string.IsNullOrEmpty(composition)) return "";

                List<SimpleMaterial> simpleMaterials = new List<SimpleMaterial>();
                var matches = Regex.Matches(composition, @"([a-zA-Z]+)([0-9]+([.]{1}[0-9]+){0,1})");
                foreach (Match item in matches)
                {
                    string element_name = item.Groups[1].Value;
                    var sm = new SimpleMaterial { ElementName = element_name, UnitPrice = 0, Weight = 0 };
                    if (useDB)
                    {
                        double unit_price = GetUnitPrice(sm.ElementName);
                        sm.UnitPrice = unit_price;
                    }
                    simpleMaterials.Add(sm);
                }

                s = SimpleMaterialToStr(simpleMaterials);
            }
            catch (Exception)
            {

            }
            return s;
        }

        /// <summary>
        /// 从单质价格数据库里获取该元素当前价格
        /// </summary>
        /// <param name="element_name"></param>
        /// <returns></returns>
        private static double GetUnitPrice(string element_name)
        {
            double unit_price = 0;
            if (string.IsNullOrEmpty(element_name)) return unit_price;
            try
            {
                using (var s = new SimpleMaterialServiceClient())
                {
                    var result = s.GetSimpleMaterialByComposition(element_name);
                    if (result != null)
                    {
                        unit_price = result.UnitPrice;
                    }

                }
            }
            catch (Exception)
            {

            }
            return unit_price;
        }

        public static double GetAllMaterialPrice(string str)
        {
            var simpleMaterials = StrToSimpleMaterial(str);
            double total_cost = 0;
            foreach (var item in simpleMaterials)
            {
                total_cost += item.UnitPrice * item.Weight;
            }
            return total_cost;
        }


        public static List<SimpleMaterial> StrToSimpleMaterial(string str)
        {
            List<SimpleMaterial> simpleMaterials = new List<SimpleMaterial>();
            try
            {
                string[] pairs = str.Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries);
                foreach (var item in pairs)
                {
                    string[] pair = item.Split(new string[] { "+" }, StringSplitOptions.RemoveEmptyEntries);

                    if (pair.Length >= 3)
                    {
                        SimpleMaterial sm = new SimpleMaterial();
                        sm.ElementName = pair[0];
                        sm.UnitPrice = double.Parse(pair[1]);
                        sm.Weight = double.Parse(pair[2]);
                        simpleMaterials.Add(sm);
                    }
                }
            }
            catch (Exception)
            {

            }

            return simpleMaterials;
        }


        public static string SimpleMaterialToStr(List<SimpleMaterial> simpleMaterials)
        {
            StringBuilder sb = new StringBuilder();
            if (simpleMaterials != null)
            {
                foreach (var item in simpleMaterials)
                {
                    if (!string.IsNullOrEmpty(item.ElementName))
                    {
                        sb.Append(item.ElementName);
                        sb.Append("+");
                        sb.Append(item.UnitPrice);
                        sb.Append("+");
                        sb.Append(item.Weight);
                        sb.Append(";");
                    }
                }
            }

            return sb.ToString();
        }


    }
}
