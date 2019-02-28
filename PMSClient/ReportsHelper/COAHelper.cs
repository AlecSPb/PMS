using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMSClient.ReportsHelper
{
    public static class COAHelper
    {
        public static string StandardizeDimension(string dimension)
        {
            if (dimension.Contains("thick"))
                return dimension;
            return dimension + " thick";
        }

        public static string GetCreateDateFromPMINumber(string pminumber)
        {
            if (pminumber.StartsWith("CD"))
            {
                return $"{pminumber.Substring(4, 2)}/{pminumber.Substring(6, 2)}/20{pminumber.Substring(2, 2)}";
            }
            else
            {
                return "unknown";
            }
        }
    }
}
