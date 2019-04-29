using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PMSClient.MainService;

namespace PMSClient.CheckLogic
{
    public static class RecordTestCheckLogic
    {
        public static void ShowWarningDialog(string warningmsg)
        {
            var win = new Tool.WarningDialog();
            win.WarningMessage = warningmsg;
            win.ShowDialog();
        }


        public static CheckResult IsDensityOK(string abbr, double density_actual)
        {
            var check = new CheckResult();
            check.IsCheckOK = true;
            check.Message = "没有检验密度";
            if (CheckElementExists(abbr, "Cu-In-Ga-Se") || abbr.Contains("CIGS"))
            {
                check.IsCheckOK = density_actual >= 5.65;
                check.Message = "Cu-In-Ga-Se的密度一般≥5.65g/cm3";
            }
            else if (CheckElementExists(abbr, "Se-As-Ge"))
            {
                check.IsCheckOK = density_actual >= 4.3;
                check.Message = "Se-As-Ge的密度一般≥4.3g/cm3";
            }
            else if (abbr.Contains("InS"))
            {
                check.IsCheckOK = density_actual >= 4.4;
                check.Message = "In-S的密度一般≥4.4g/cm3";
            }
            else if (abbr.Contains("InSe"))
            {
                check.IsCheckOK = density_actual >= 5.4;
                check.Message = "In-Se的密度一般≥5.4g/cm3";
            }
            return check;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="abbr"></param>
        /// <param name="elements">用-作为连字符</param>
        /// <returns></returns>
        private static bool CheckElementExists(string abbr, string elements_string)
        {
            string[] elements = elements_string.Split(new char[] { '-' },
                StringSplitOptions.RemoveEmptyEntries);
            foreach (var item in elements)
            {
                if (!abbr.Contains(item))
                    return false;
            }
            return true;
        }

        public static bool IsProductIDUnique(string productid)
        {
            //检查是否存在相同ID
            using (var service = new RecordTestServiceClient())
            {
                var recordtest = service.GetRecordTestByProductID(productid).FirstOrDefault();
                return recordtest == null;
            }
        }

        public static bool IsProductIDLogic(string productid, string dimension)
        {

            if (dimension.Trim().StartsWith("230"))
            {
                return DateTime.Now.AddDays(-2).ToString("yyMMdd")
                    .CompareTo(productid.Substring(0, 6))>=0;
            }
            else
            {
                return DateTime.Now.AddDays(-1).ToString("yyMMdd")
                    .CompareTo(productid.Substring(0, 6)) >= 0;
            }
        }

        public static bool IsBridgeLineCompositionOK(string customerName,string composition)
        {
            if (customerName.ToLower().Contains("bridgeline"))
            {
                int line_count = composition.Split('\r').Length;
                return line_count >= 15;
            }
            return true;
        }
    }


}
