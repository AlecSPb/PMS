using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PMSWCFService.ServiceImplements.Helpers
{
    public class CompositionHelper
    {
        /// <summary>
        /// 分解搜索字符串
        /// </summary>
        /// <param name="searchStr"></param>
        /// <returns></returns>
        public static SearchItem GetSearchItems(string searchStr)
        {
            SearchItem searchItem = new SearchItem();
            if (string.IsNullOrEmpty(searchStr))
            {
                return searchItem;
            }

            string[] items = searchStr.Split(new string[] { "-" }, StringSplitOptions.RemoveEmptyEntries);

            searchItem.Item1 = GetStringArrayAt(items, 0);
            searchItem.Item2 = GetStringArrayAt(items, 1);
            searchItem.Item3 = GetStringArrayAt(items, 2);
            searchItem.Item4 = GetStringArrayAt(items, 3);



            return searchItem;
        }

        private static string GetStringArrayAt(string[] items, int index)
        {
            if (items == null) return "";
            if (index < items.Length)
            {
                return items[index];
            }
            else
            {
                return "";
            }
        }

    }
}