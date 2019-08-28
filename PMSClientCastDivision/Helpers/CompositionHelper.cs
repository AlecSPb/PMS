using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.IO;

namespace PMSClient.Helpers
{
    public static class CompositionHelper
    {
        /// <summary>
        /// 利用正则表达式返回成分里的每个元组的名称
        /// </summary>
        /// <param name="composition"></param>
        /// <returns></returns>
        public static List<string> GetElements(string composition)
        {
            if (string.IsNullOrEmpty(composition))
                return null;
            string pattern = @"[a-zA-Z]+";
            var matches = Regex.Matches(composition, pattern);

            List<string> results = new List<string>();
            foreach (Group item in matches)
            {
                results.Add(item.Value);
            }
            return results;
        }

    }
}
