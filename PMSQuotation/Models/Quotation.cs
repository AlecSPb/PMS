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
        public BasicInfo BasicInformation { get; set; }//json
        public CustomerInfo CustomerInformation { get; set; }//json

        public string Number { get; set; }
        public string Title { get; set; }
        public List<QuotationUnit> Units { get; set; }//json

        public double ExtraFee { get; set; }

        public double ServiceFee { get; set; }


        public double TotalFee { get; set; }

        public string MoneyType { get; set; }

        public string TotalType { get; set; }

        public DateTime CreateTime { get; set; }
        public DateTime LastUpdateTime { get; set; }
        public string Creator { get; set; }

        public string GeneralClause { get; set; }
        public string LegalClause { get; set; }
        public string SpecialClause { get; set; }

        public DeliveryContent DeliveryContent { get; set; }

        public DateTime ExpirationDate { get; set; }
    }
}
