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
        public CalculationService()
        {
            db_service = new QuotationDbService();
        }

        private QuotationDbService db_service;
        public Tuple<double, double, double, string> GetTotalCost(Quotation model)
        {
            if (model == null) return Tuple.Create(0.0, 0.0, 0.0, model.CurrencyType);

            var quotation_items = db_service.GetQuotationItems(model.ID);

            if (quotation_items.Count == 0) Tuple.Create(0.0, 0.0, 0.0, model.CurrencyType);

            double extra_fee = 0;
            double target_fee = 0;
            extra_fee = model.PackageFee + model.ShippingFee + model.CustomFee + model.TaxFee;

            foreach (var item in quotation_items)
            {
                target_fee += item.UnitPrice * item.Quantity;
            }


            return Tuple.Create(target_fee + extra_fee, target_fee, extra_fee, model.CurrencyType);
        }

        public double GetTotalQuotationItems(List<QuotationItem> models)
        {

            return 0;
        }

        public double GetRawMaterial(double unit_price, double weight)
        {
            return unit_price * weight;
        }

        public double GetPowderPrice(double unit_price, double weight)
        {
            return unit_price * weight;
        }


    }
}
