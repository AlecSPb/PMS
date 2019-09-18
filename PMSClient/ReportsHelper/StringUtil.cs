using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMSClient.ReportsHelper
{
    public static class StringUtil
    {
        public static string RemoveSlash(string str)
        {
            return str.Replace("\\", "")
                      .Replace("/", "")
                      .Replace("%", "")
                      .Replace("<", "")
                      .Replace(">", "")
                      .Replace(":", "")
                      .Replace("*", "");

        }
    }
}
