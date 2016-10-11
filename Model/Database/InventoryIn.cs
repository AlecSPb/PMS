using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    /// <summary>
    /// 库存表In
    /// 暂时只考虑简单的出入库流水账记录
    /// </summary>
    public class InventoryIn
    {
        public Guid ID { get; set; }
        public string ItemName { get; set; }
        public double Quantity { get; set; }
        public string Unit { get; set; }
        public string Purity { get; set; }

        public string ComeFrom { get; set; }
        public string ExtraInformation { get; set; }

        public string Creator { get; set; }
        public DateTime CreateDate { get; set; }

    }
}
