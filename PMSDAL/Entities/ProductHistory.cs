using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMSDAL
{
    public class ProductHistory
    {
        public Guid ID { get; set; }
        public DateTime CreateTime { get; set; }
        public string Creator { get; set; }
        public string State { get; set; }

        public string ProductType { get; set; }//裸靶 or Bonding or其他
        public string ProductID { get; set; }
        public string Composition { get; set; }
        public string Abbr { get; set; }
        public string Customer { get; set; }
        public string PO { get; set; }
        public string Weight { get; set; }
        public string Dimension { get; set; }
        public string DimensionActual { get; set; }
        public string Defects { get; set; }
        public string DetailRecord { get; set; }//复杂的信息写在这里
        public string Position { get; set; }//入库库房编号，位置编号，unknown，成品库房 产品架A


        //操作者和操作时间
        public string Operator { get; set; }
        public string OperateTime { get; set; }

    }
}
