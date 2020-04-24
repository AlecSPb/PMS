using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMSDAL
{
    /// <summary>
    /// 耗材使用记录
    /// </summary>
    public class ConsumableInventory : ModelBase
    {
        public string ItemName { get; set; }//名称
        public string ItemDetails { get; set; }//细节
        public double Quantity { get; set; }//数量
        public string QuantityUnit { get; set; }//单位
        public string Category { get; set; }//类别

        public string UpdatePerson { get; set; }//更新人

        public double MinWarningQuantity { get; set; }//最小报警量

        public string Remark { get; set; }
    }
}
