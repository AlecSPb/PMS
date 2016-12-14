using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    /// <summary>
    /// 正式原料订单的公共部分
    /// </summary>
    public class MaterialOrder
    {
        public Guid ID { get; set; }
        public string OrderLot { get; set; }
        public string Supplier { get; set; }
        public string VendorInformation { get; set; }
        public string ShipToAddress { get; set; }
        public string ExtraInformation { get; set; }




        public int CurrentState { get; set; }
        public string Creator { get; set; }
        public DateTime CreateTime { get; set; }

    }


}
