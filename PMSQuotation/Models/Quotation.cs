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
        public DateTime CreateTime { get; set; }
        public DateTime LastUpdateTime { get; set; }
        public DateTime ExpirationTime { get; set; }
        public string Creator { get; set; }

        public string State { get; set; }
        public string Lot { get; set; }
        public string Remark { get; set; }
        public string KeyWord { get; set; }//方便检索

        #endregion

        #region CustomerInfo
        public string Customer_CompanyName { get; set; }
        public string Customer_Address { get; set; }
        public string Customer_PostCode { get; set; }
        public string Customer_Contact { get; set; }
        public string Customer_Phone { get; set; }
        public string Customer_Email { get; set; }
        public string Customer_Fax { get; set; }
        #endregion


        #region BasicInfo
        public string Basic_CompanyName { get; set; }
        public string Basic_Address { get; set; }
        public string Basic_PostCode { get; set; }
        public string Basic_Contact { get; set; }
        public string Basic_Phone { get; set; }
        public string Basic_Email { get; set; }
        public string Basic_Fax { get; set; }
        #endregion




        #region QuotationContent

        public List<QuotationItem> Items { get; set; }

        public double PackageFee { get; set; }
        public string PackageRemark { get; set; }
        public double ShippingFee { get; set; }
        public string ShippingRemark { get; set; }
        public double CustomFee { get; set; }
        public string CustomRemark { get; set; }
        public double TaxFee { get; set; }
        public string TaxRemark { get; set; }

        #endregion




    }
}
