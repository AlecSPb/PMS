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
    }
}
