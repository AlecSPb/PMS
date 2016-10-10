using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    /// <summary>
    /// 发货单表
    /// </summary>
    public class Delivery
    {
        public Guid ID { get; set; }
        public string ShipSheetLot { get; set; }

        public string ShipToAddress { get; set; }
        public string PakcageRequirement { get; set; }




        public string Creator { get; set; }
        public DateTime CreateTime { get; set; }


    }
}
