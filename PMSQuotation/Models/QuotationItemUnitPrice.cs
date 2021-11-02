using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMSQuotation.Models
{
    /// <summary>
    /// 1片的费用
    /// </summary>
    public class QuotationItemUnitPrice
    {
        public int ID { get; set; }
        public string ItemName { get; set; }
        public string ItemPrice { get; set; }
        public string ItemDetail { get; set; }
        public string ItemNote { get; set; }
    }
}
