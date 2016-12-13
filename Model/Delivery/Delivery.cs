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


        public string InvoiceNumber { get; set; }
        public string PackageStatus { get; set; }

        public string DeliveryName { get; set; }
        public string DeliveryNumber { get; set; }

        public string Country { get; set; }
        public string ShipAddress { get; set; }
        public DateTime ShipTime { get; set; }

        public List<DeliveryItem> DeliveryItems { get; set; }

        public string Remark { get; set; }

        public string PackageType { get; set; }//木箱 纸箱 塑料箱
        public string PackageInformation { get; set; }

        public int State { get; set; }//取消，未审核，审核通过
    }
}
