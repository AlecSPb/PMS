using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMSDAL
{
    public class PMSMaterialOrder
    {
        public PMSMaterialOrder()
        {
            MaterialOrderItems = new List<PMSMaterialOrderItem>();
        }
        public Guid ID { get; set; }
        public DateTime CreateTime { get; set; }
        public string Creator { get; set; }
        public int State { get; set; }


        public string OrderPO { get; set; }
        public string Supplier { get; set; }
        public string SupplierAbbr { get; set; }
        public string SupplierReceiver { get; set; }
        public string SupplierEmail { get; set; }
        public string SupplierAddress { get; set; }

        public string Remark { get; set; }

        public double ShipFee { get; set; }

        public virtual List<PMSMaterialOrderItem> MaterialOrderItems { get; set; }
    }
}
