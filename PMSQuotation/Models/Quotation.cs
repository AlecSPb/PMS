using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMSQuotation.Models
{
    /// <summary>
    /// 核心报价类
    /// </summary>
    public class Quotation
    {
        public int ID { get; set; }
        #region SheetInfo
        public string CurrencyType { get; set; }
        public double TotalCost { get; set; }
        public double Discount { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime LastUpdateTime { get; set; }
        public DateTime ExpirationTime { get; set; }
        public string Creator { get; set; }

        public string State { get; set; }
        public string Lot { get; set; }
        public string Remark { get; set; }
        public string RFQNumber { get; set; }
        public string Terms { get; set; }
        public string ShipVia { get; set; }
        public string KeyWord { get; set; }//方便检索

        #endregion

        #region CustomerInfo
        public string ContactInfo_Customer { get; set; }
        #endregion


        #region BasicInfo
        public string ContactInfo_Self { get; set; }
        #endregion




        #region QuotationContent

        public List<QuotationItem> Items { get; set; }

        public double PackageFee { get; set; }
        public string PackageRemark { get; set; }
        public double ShippingFee { get; set; }
        public string ShippingRemark { get; set; }
        public double CustomFee { get; set; }
        public string CustomRemark { get; set; }

        public bool IsAutoTax { get; set; }
        public double TaxFee { get; set; }
        public string TaxRemark { get; set; }

        #endregion




    }
}
