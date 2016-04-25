using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UsefulPackage
{
    public static class PMTranslate
    {
        /// <summary>
        /// 生成热压靶材的Lot号码
        /// </summary>
        /// <param name="vhpDate">热压日期</param>
        /// <param name="deviceTypeCode">热压设备代码</param>
        /// <param name="vhpCount">一次的热压次数</param>
        /// <returns>Lot字符串</returns>
        public static string GetTargetLot(DateTime vhpDate, string deviceTypeCode = "unknown", int vhpCount = 1)
        {
            string part1 = vhpDate.ToString("yyMMdd");
            string part2 = deviceTypeCode;
            string part3 = "1";
            for (int i = 1; i < vhpCount; i++)
            {
                part3 += ",";
                part3 += (i + 1).ToString();
            }
            return part1 + "-" + part2 + "-" + part3;
        }


    }
}
