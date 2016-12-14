using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    /// <summary>
    /// 库存表Out
    /// 创建出库表的时候基本信息可以来自入库表
    /// </summary>
    public class IMaterialOut
    {
        public Guid ID { get; set; }
        public string ItemName { get; set; }
        public double Quantity { get; set; }
        public string Unit { get; set; }
        public string Purity { get; set; }

        public string GoTo { get; set; }
        public string ExtraInformation { get; set; }

        public string Creator { get; set; }
        public DateTime CreateTime { get; set; }
    }
}
