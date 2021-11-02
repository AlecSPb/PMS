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
        public Guid ID { get; set; }

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


        #region SheetInfo
        public string CurrencyType { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime LastUpdateTime { get; set; }
        public string Creator { get; set; }
        public DateTime ExpirationTime { get; set; }

        public string Lot { get; set; }
        public string KeyWord { get; set; }

        #endregion

        #region QuotationContent
        
        public List<QuotationItem> Items { get; set; }

        #endregion


    }
}
