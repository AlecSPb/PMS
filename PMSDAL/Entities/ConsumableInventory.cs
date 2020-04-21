using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMSDAL
{
    /// <summary>
    /// 耗材使用记录
    /// </summary>
    public class ConsumableInventory : ModelBase
    {
        public string ItemName { get; set; }
        public string ItemDetails { get; set; }
        public double Quantity { get; set; }
        public string QuantityUnit { get; set; }

        public double MinWarningQuantity { get; set; }

        public string Remark { get; set; }
    }
}
