using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    /// <summary>
    /// 正式原料订单的具体项目
    /// </summary>
    public class MaterialOrderItem
    {
        public Guid ID { get; set; }
        public Guid MaterialOrderID { get; set; }

        public string Composition { get; set; }
        public string Purity { get; set; }
        public double Weight { get; set; }
        public string PMIWorkingNumber { get; set; }
        public string Description { get; set; }

        public double UnitPrice { get; set; }
    }
}
