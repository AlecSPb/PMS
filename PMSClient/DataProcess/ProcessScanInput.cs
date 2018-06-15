using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMSClient.DataProcess
{

    public enum DataSource
    {
        Test,
        Plate,
        Outsource,
        Unknown
    }

    /// <summary>
    /// 处理扫描输入
    /// </summary>
    public class ProcessScanInput
    {
        public ProcessScanInput()
        {

        }

        /// <summary>
        /// 返回数据源类型
        /// </summary>
        /// <param name="lot"></param>
        /// <returns></returns>
        public DataSource GetDSType(string lot)
        {
            if (lot.Length >= 9)
            {
                string mark = lot.Substring(7, 2).Replace("-", "");
                switch (mark.ToUpper())
                {
                    case "OS":
                        return DataSource.Outsource;
                    case "BP":
                        return DataSource.Plate;
                    default:
                        return DataSource.Test;
                }
            }
            else
            {
                return DataSource.Unknown;
            }
        }

        /// <summary>
        /// 拆分换行
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public string[] SplitMultiLineText(string text)
        {
            return text.Split(Environment.NewLine.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
        }


        public string CheckAll(string product_lots)
        {
            if (string.IsNullOrEmpty(product_lots))
                return "";

            string[] data = SplitMultiLineText(product_lots);
            StringBuilder sb = new StringBuilder();
            foreach (var line in data)
            {
                sb.Append(line);
                sb.AppendLine(" " + GetDSType(line).ToString());
            }
            return sb.ToString();
        }


    }
}
