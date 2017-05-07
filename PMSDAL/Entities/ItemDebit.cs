using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMSDAL
{
    /// <summary>
    /// 物品来往记账
    /// </summary>
    public class ItemDebit
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
        public double UnitPrice { get; set; }
        public string Creditor { get; set; }
        public double Remark { get; set; }
    }
}
