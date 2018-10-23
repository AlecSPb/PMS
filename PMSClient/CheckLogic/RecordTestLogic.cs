using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMSClient.CheckLogic
{
    public static class RecordTestLogic
    {
        public static CheckMessage IsDensityOK(string abbr, double density_actual)
        {
            var check = new CheckMessage();
            check.isCheckOK = false;
            if (CheckElementExists(abbr, "Cu-In-Ga-Se") || abbr.Contains("CIGS"))
            {
                check.isCheckOK = density_actual > 5.65;
                check.Message = "Cu-In-Ga-Se的密度一般大于5.65g/cm3";
            }
            else if (CheckElementExists(abbr, "Se-As-Ge"))
            {
                check.isCheckOK = density_actual > 4.3;
                check.Message = "Se-As-Ge的密度一般大于4.3";
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
    }


}
