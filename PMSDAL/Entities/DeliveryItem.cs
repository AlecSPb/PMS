using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PMSDAL
{
    /// <summary>
    /// 送货单项目
    /// 不涉及过多产品具体信息，在所有产品和样品之间通用
    /// </summary>
    public class DeliveryItem
    {
        [Key]
        public Guid ID { get; set; }
        public DateTime CreateTime { get; set; }
        public string Creator { get; set; }
        public string ProductType { get; set; }//裸靶 or Bonding or其他
        public string ProductID { get; set; }
        public string Composition { get; set; }
        public string Abbr { get; set; }
        public string Customer { get; set; }
        public string PO { get; set; }
        public string Weight { get; set; }
        public string DetailRecord { get; set; }//复杂的信息写在这里

        public int PackNumber { get; set; }//箱子号
        public string Position { get; set; }//入库库房编号，位置编号，unknown，成品库房 产品架A

        public string Remark { get; set; }

        public string State { get; set; }

        public Guid DeliveryID { get; set; }
    }
}
