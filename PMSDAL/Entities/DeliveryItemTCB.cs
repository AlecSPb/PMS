using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMSDAL
{
    public class DeliveryItemTCB
    {
        [Key]
        public Guid ID { get; set; }
        public DateTime CreateTime { get; set; }
        public string Creator { get; set; }
        public string ProductType { get; set; }//裸靶 or Bonding or其他
        public string ProductID { get; set; }
        public string Composition { get; set; }
        public string Abbr { get; set; }
        public string Customer { get; set; }
        public string PO { get; set; }
        public string Weight { get; set; }
        public string DetailRecord { get; set; }//复杂的信息写在这里
        public string Dimension { get; set; }
        public string DimensionActual { get; set; }
        public string Defects { get; set; }
        public string Remark { get; set; }




        public string BondingPO { get; set; }
        public string DeliveryName { get; set; }
        public string ExpressName { get; set; }
        public string ExpressNumber { get; set; }
        public string TrackingHistory { get; set; }

        public string State { get; set; }
    }
}
