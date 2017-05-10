using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMSDAL
{
    /// <summary>
    /// 物品来往记账
    /// </summary>
    public class ItemDebitHistory
    {
        public Guid ID { get; set; }
        public string Creator { get; set; }
        public DateTime CreateTime { get; set; }
        public string State { get; set; }

        public string ItemType { get; set; }//材料还是物品
        public string ItemLot { get; set; }//编号
        public string ItemName { get; set; }//项目
        public string ItemProperty { get; set; }
        public double Quantity { get; set; }
        public double Unit { get; set; }
        public double UnitPrice { get; set; }
        public string Creditor { get; set; }
        public double Remark { get; set; }

        //操作者和操作时间
        [Key]
        public Guid HistoryID { get; set; }
        public string Operator { get; set; }
        public DateTime OperateTime { get; set; }
    }
}
