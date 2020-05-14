using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMSEOrder.Service
{
    public static class StringUtil
    {
        /// <summary>
        /// 移除文件名里Window不支持的内容
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
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
