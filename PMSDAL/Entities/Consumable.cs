using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMSDAL
{
    public class Consumable
    {
        public Guid ID { get; set; }
        public string Creator { get; set; }
        public string CreateTime { get; set; }
        public string State { get; set; }


        public string ItemName { get; set; }
        public string ItemDetails { get; set; }
        public double Quantity { get; set; }
        public string QuantityUnit { get; set; }

        public string ForWho { get; set; }
        public string SupplierSource { get; set; }


        public double Cost { get; set; }

        public string Remark { get; set; }


    }
}
