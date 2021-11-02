using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PMSQuotation.Models;

namespace PMSQuotation.Services
{
    /// <summary>
    /// 计算服务类
    /// </summary>
    public class CalculationService
    {
        
        public double GetTotalCost(Quotation model)
        {
            return 0;
        }

        public double GetTotalQuotationItems(List<QuotationItem> models)
        {

            return 0;
        }

        public double GetRawMaterial(double unit_price,double weight)
        {
            return unit_price * weight;
        }

        public double GetPowderPrice(double unit_price,double weight)
        {
            return unit_price * weight;
        }


    }
}
