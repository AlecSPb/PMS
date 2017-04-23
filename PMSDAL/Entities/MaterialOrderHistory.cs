using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMSDAL
{
    public class MaterialOrderHistory
    {
        public Guid ID { get; set; }
        public DateTime CreateTime { get; set; }
        public string Creator { get; set; }
        public string State { get; set; }


        public string OrderPO { get; set; }
        public string Supplier { get; set; }
        public string SupplierAbbr { get; set; }
        public string SupplierReceiver { get; set; }
        public string SupplierEmail { get; set; }
        public string SupplierAddress { get; set; }

        public string Remark { get; set; }

        public double ShipFee { get; set; }

        public string Priority { get; set; }

        //操作者和操作时间
        [Key]
        public Guid HistoryID { get; set; }
        public string Operator { get; set; }
        public DateTime OperateTime { get; set; }
    }
}
