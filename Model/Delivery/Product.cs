using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Delivery
{
    public class Product
    {
        public Guid ID { get; set; }
        public string ProductID { get; set; }
        public string Composition { get; set; }
        public string PO { get; set; }
        public string Dimension { get; set; }
        public string Remark { get; set; }
    }
}
