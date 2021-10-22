using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMSQuotation.Models
{
    public class DeliveryContent
    {
        public Guid ID { get; set; }
        public DateTime DeliveryTime { get; set; }
        public string Address { get; set; }
 
    }
}
