using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMSClient.Components.EOrder.Midsummer
{
    /// <summary>
    /// 仅存取从xml中获取的有用信息
    /// </summary>
    public class MidOrder
    {
        public string CustomerName { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime DeliveryDate { get; set; }

        public string PO { get; set; }
        public string Composition { get; set; }
        public string Dimension { get; set; }
        public int Quantity { get; set; }


        public string Remark { get; set; }
    }
}
