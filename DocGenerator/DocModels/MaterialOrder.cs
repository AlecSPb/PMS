using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocGenerator.DocModels
{
    public class MaterialOrder
    {
        public MaterialOrder()
        {
            MaterialOrderItems = new List<DocModels.MaterialOrderItem>();
        }
        public Guid ID { get; set; }
        public DateTime CreateTime { get; set; }
        public string Creator { get; set; }
        public string State { get; set; }


        public string OrderPO { get; set; }
        public string Supplier { get; set; }
        public string SupplierAbbr { get; set; }
        public string SupplierReceiver { get; set; }
        public string SupplierEmail { get; set; }
        public string SupplierAddress { get; set; }

        public string Remark { get; set; }

        public double ShipFee { get; set; }
        public string Priority { get; set; }

        public List<MaterialOrderItem> MaterialOrderItems { get; set; }
    }
}
