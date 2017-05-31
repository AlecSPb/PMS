using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMSDAL
{
    public class OutSource
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
        [DefaultValue("未付款")]
        public string PaidState { get; set; }
        public string Remark { get; set; }



    }
}
