using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMSQuotation.Services
{
    public class QuotationHelper
    {
        /// <summary>
        /// 获取默认Lot号
        /// </summary>
        /// <returns></returns>
        public static string GetDefaultLot()
        {
            return $"PMI{DateTime.Now.ToString()}";
        }




    }
}
