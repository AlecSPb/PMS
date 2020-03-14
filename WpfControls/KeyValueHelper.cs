using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFControls
{
    public class KeyValueHelper
    {

        public static List<KeyValue> StrToKeyValues(string Str)
        {
            List<KeyValue> keyValues = new List<KeyValue>();
            try
            {
                string[] pairs = Str.Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries);
                foreach (var item in pairs)
                {
                    string[] pair = item.Split(new string[] { "=" }, StringSplitOptions.RemoveEmptyEntries);
                    KeyValue keyValue = new KeyValue { Key = pair[0], Value = pair[1] };
                    keyValues.Add(keyValue);
                }
            }
            catch (Exception)
            {

            }
            return keyValues;
        }

        public static string KeyValuesToStr(List<KeyValue> keyValues)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var item in keyValues)
            {
                if (!string.IsNullOrEmpty(item.Key)&&!string.IsNullOrEmpty(item.Value))
                {
                    sb.Append(item.Key);
                    sb.Append("=");
                    sb.Append(item.Value);
                    sb.Append(";");
                }
            }
            return sb.ToString();
        }

    }
}
