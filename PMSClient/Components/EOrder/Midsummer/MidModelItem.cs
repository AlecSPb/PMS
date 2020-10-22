using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMSClient.Components.EOrder.Midsummer
{
    public class MidModelItem
    {
        public int RowNumber { get; set; }
        public string RowType { get; set; }

        public string PartNumber { get; set; }
        public string SupplierPartNumber { get; set; }

        public string Text { get; set; }

        public string ReferenceNumber { get; set; }
        public int Quantity { get; set; }

        public string Unit { get; set; }

        public DateTime DeliveryPeriod { get; set; }

        public string Each { get; set; }
        public string Discount { get; set; }
        public string Setup { get; set; }
        public string Alloy { get; set; }



    }
}
