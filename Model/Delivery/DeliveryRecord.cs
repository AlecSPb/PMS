using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Delivery
{
    //发货单
    public class DeliveryRecord
    {
        public Guid ID { get; set; }
        public DateTime CreateTime { get; set; }
        public string Creator { get; set; }
        public int State { get; set; }

        public string PONumber { get; set; }

        public string Delivery { get; set; }
        public string DeliveryNumber { get; set; }

        public string  Desitination { get; set; }
        public DateTime DeliveryTime { get; set; }

        public List<Product> Products { get; set; }

        public string Remark { get; set; }

        public string PackageWeightStatus { get; set; }


    }
}
