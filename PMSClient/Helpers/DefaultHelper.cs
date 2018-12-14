using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMSClient.Helpers
{
    public static class DefaultHelper
    {
        public static string DefaultPMINumber()
        {
            return $"CD{DateTime.Now.ToString("yyMMdd")}-A";
        }
    }
}
