using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PMSQuotation.Models;

namespace PMSQuotation.Helpers
{
    public class QuotationHelper
    {
        /// <summary>
        /// 获取默认Lot号
        /// </summary>
        /// <returns></returns>
        public static string GetDefaultLot()
        {
            return $"PMI{DateTime.Now.ToString("yyyyMMddHHmmss")}";
        }

        public static List<string> GetQuotationStates()
        {
            List<string> states = new List<string>();
            foreach (var item in Enum.GetValues(typeof(QuotationState)))
            {
                states.Add(item.ToString());
            }
            return states;
        }

        public static List<string> GetQuotationItemStates()
        {
            List<string> states = new List<string>();
            foreach (var item in Enum.GetValues(typeof(QuotationItemState)))
            {
                states.Add(item.ToString());
            }
            return states;
        }
    }
}
