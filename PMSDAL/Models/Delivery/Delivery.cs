using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMSDAL
{
    //发货单
    public class Delivery
    {
        public Delivery()
        {
            DeliveryItems = new List<PMSDAL.DeliveryItem>();
        }
        [Key]
        public Guid ID { get; set; }
        public DateTime CreateTime { get; set; }
        public string Creator { get; set; }

        public string InvoiceNumber { get; set; }

        public string DeliveryName { get; set; }
        public string DeliveryNumber { get; set; }

        public string Country { get; set; }
        public string Address { get; set; }
        public DateTime ShipTime { get; set; }
        public string Remark { get; set; }
        public string PackageType { get; set; }//木箱 纸箱 塑料箱
        public string PackageInformation { get; set; }//包装重量等细节信息
        public int State { get; set; }//取消，未审核，审核通过，已发货
        
        public virtual List<DeliveryItem> DeliveryItems { get; set; }
    }
}
