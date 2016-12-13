using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    //发货单
    public class Delivery
    {
        public Guid ID { get; set; }
        public DateTime CreateTime { get; set; }
        public string Creator { get; set; }
        public int State { get; set; }//取消，未审核，审核通过

        public string InvoiceNumber { get; set; }
        public string PackageStatus { get; set; }

        public string Delivery { get; set; }
        public string DeliveryNumber { get; set; }

        public string Country { get; set; }
        public string ShipAddress { get; set; }
        public DateTime ShipTime { get; set; }

        public List<DeliveryItem> DeliveryItems { get; set; }

        public string Remark { get; set; }

        public string PackageWeightStatus { get; set; }


    }
}
