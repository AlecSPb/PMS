using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMSDAL
{
    public class OutSourceHistory
    {
        public Guid ID { get; set; }
        public string Creator { get; set; }
        public DateTime CreateTime { get; set; }
        public string State { get; set; }
        public string OrderLot { get; set; }
        public string OrderType { get; set; }
        public string OrderName { get; set; }
        public string Supplier { get; set; }
        public string Dimension { get; set; }
        public double Quantity { get; set; }
        public string QuantityUnit { get; set; }
        public double Cost { get; set; }

        public DateTime FinishTime { get; set; }
        public string PaidState { get; set; }
        public string Remark { get; set; }

        //操作者和操作时间
        [Key]
        public Guid HistoryID { get; set; }
        public string Operator { get; set; }
        public DateTime OperateTime { get; set; }

    }
}
