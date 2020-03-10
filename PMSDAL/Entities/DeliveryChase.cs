using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMSDAL
{
    /// <summary>
    /// 货物交付追踪-
    /// </summary>
    public class DeliveryChase:ModelBase
    {
        public string ProductID { get; set; }
        public string Composition { get; set; }
        public string PO { get; set; }
        public string PMINumber { get; set; }
        public string Customer  { get; set; }
        public string Dimension { get; set; }
        public double Weight { get; set; }
        public string TraceInformation { get; set; }

        public string Remark { get; set; }

    }
}
