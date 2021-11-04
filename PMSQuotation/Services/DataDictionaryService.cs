using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMSQuotation.Services
{
    /// <summary>
    /// 数据字典帮助类
    /// </summary>
    public class DataDictionaryService
    {
        public DataDictionaryService()
        {
            db_service = new QuotationDbService();
        }

        public QuotationDbService db_service { get; set; }

        public string GetString(string key)
        {
            return db_service.GetDataDictByKey(key).DataValue;
        }
        public double GetDouble(string key)
        {
            string s = db_service.GetDataDictByKey(key).DataValue;
            double value = 0;
            double.TryParse(s, out value);
            return value;
        }
        public int GetInt(string key)
        {
            string s = db_service.GetDataDictByKey(key).DataValue;
            int value = 0;
            int.TryParse(s, out value);
            return value;
        }

        public Dictionary<string, double> GetKeyValue(string key)
        {
            Dictionary<string, double> dicts = new Dictionary<string, double>();
            string s = db_service.GetDataDictByKey(key).DataValue;

            string[] groups = s.Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries);

            if (groups.Length > 0)
            {
                foreach (var group in groups)
                {
                    string[] strs = group.Split(new string[] { "=" }, StringSplitOptions.RemoveEmptyEntries);
                    if (strs.Length >= 2)
                    {
                        string t_key = strs[0];
                        double t_value = 0;
                        double.TryParse(strs[1], out t_value);
                        dicts.Add(t_key, t_value);
                    }

                }
            }

            return dicts;
        }

    }
}
