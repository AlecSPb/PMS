using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMSQuotation.Models
{
    public class QuotationUnit
    {
        public Guid ID { get; set; }
        public string ItemType { get; set; }
        public string ItemContent { get; set; }
        public double Price { get; set; }
        public double Tax { get; set; }
        
    }
}
