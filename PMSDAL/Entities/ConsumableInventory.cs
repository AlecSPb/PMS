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
        public string Category { get; set; }//类别
        public string ItemName { get; set; }//名称
        public string Specification { get; set; }//名称
        public string Details { get; set; }//细节
        public double Quantity { get; set; }//数量
        public string QuantityUnit { get; set; }//单位

        public string Grade { get; set; }//ABC级别
        public string StorePosition { get; set; }//存储位置
        public string PersonInCharge { get; set; }//负责人
        public double MaxWarningQuantity { get; set; }//最大报警量
        public double MinWarningQuantity { get; set; }//最小报警量

        public string History { get; set; }//存储历史
        public string Remark { get; set; }
        public string LastUpdateTime { get; set; }//最后一次编辑时间
    }
}
