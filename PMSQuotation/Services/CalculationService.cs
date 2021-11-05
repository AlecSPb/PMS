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

        public CalculationResult Calculate(Quotation model)
        {
            CalculationResult result = new CalculationResult();
            if (model != null)
            {
                result.ExtraFee = model.PackageFee + model.ShippingFee + model.CustomFee;

                var quotation_items = db_service.GetQuotationItems(model.ID,false);
                if (quotation_items.Count != 0)
                {
                    foreach (var item in quotation_items)
                    {
                        result.TargetFee += item.UnitPrice * item.Quantity;
                    }
                }

                if (model.TaxFee != 0)
                {
                    double vat_rate = 0;
                    double.TryParse(db_service.GetDataDictByKey("vat_rate").DataValue, out vat_rate);

                    result.TaxFee = (result.TargetFee + result.ExtraFee) * vat_rate;
                }
            }


            return result;
        }

        /// <summary>
        /// 计算一组QuotationItem的TargetFee
        /// </summary>
        /// <param name="quotation_items"></param>
        /// <returns></returns>
        public double GetQuotationItemsTargetFee(List<QuotationItem> quotation_items)
        {
            double target_fee = 0;
            foreach (var item in quotation_items)
            {
                target_fee += item.UnitPrice * item.Quantity;
            }
            return target_fee;
        }

        /// <summary>
        /// 计算单个QuotationItemTotalPrice
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public double GetQuotationItemTotalPrice(QuotationItem model)
        {
            if (model == null) return 0.0;
            return model.UnitPrice * model.Quantity;
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
