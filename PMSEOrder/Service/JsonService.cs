using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using XSHelper;
using PMSEOrder.Model;

namespace PMSEOrder.Service
{
    public class JsonService
    {
        /// <summary>
        /// 从json文件获取customer
        /// </summary>
        public static List<string> GetCustomers()
        {
            List<string> result = new List<string>();
            try
            {
                string jsonStr = XS.File.ReadText(XS.File.GetCurrentFolderPath("DB") + "\\customer.json");
                var customers = JsonConvert.DeserializeObject<List<CustomerInfo>>(jsonStr);
                result = customers.Select(i => i.CustomerName)
                             .OrderBy(i => i)
                             .ToList();
            }
            catch (Exception)
            {

                throw;
            }

            return result;
        }

        public static void SaveJsonToFile(object obj, string filepath)
        {
            try
            {
                var jsonStr = JsonConvert.SerializeObject(obj, Formatting.Indented);
                XS.File.SaveText(filepath, jsonStr);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static string GetJsonFileName(Order obj)
        {
            if (obj == null) return "empty";
            return $"{obj.CreateTime.ToString("yyMMdd")} {StringUtil.RemoveSlash(obj.CustomerName)} {StringUtil.RemoveSlash(obj.Composition)} {obj.Quantity}";
        }

    }
}
