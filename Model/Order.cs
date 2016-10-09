using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Order
    {
        public Guid ID { get; set; }
        public string CustomerName { get; set; }
        public string PO { get; set; }
        public string Composition { get; set; }
        public string PMIWorkingNumber { get; set; }
        public string ProductType { get; set; }

        public DateTime OrderDate { get; set; }
        public bool OrderState { get; set; }
        public int Priority { get; set; }
    }
}
