using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMSDAL
{
    /// <summary>
    /// 耗材采购
    /// </summary>
    public class ConsumablePurchase : ModelBase
    {
        public string Category { get; set; }//类别
        public string ItemName { get; set; }//名称
        public string Specification { get; set; }
        public string Details { get; set; }//细节
        public double Quantity { get; set; }//数量
        public string QuantityUnit { get; set; }//单位
        public string Grade { get; set; }//ABC级别
        public string Remark { get; set; }

        public string Supplier { get; set; } //供应商
        public double TotalCost { get; set; } //总花费
        public string ProcessHistory { get; set; }//已购买，已到货，发货已到

        public DateTime LastUpdateTime { get; set; }//最后一次编辑时间
    }
}
