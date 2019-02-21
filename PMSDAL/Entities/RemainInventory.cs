using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMSDAL
{
    /// <summary>
    /// 用来存放多做产品库存的管理
    /// </summary>
    public class RemainInventory:ModelBase
    {
        public string ProductID { get; set; }
        public string Composition { get; set; }
        public string Dimension { get; set; }
        public string Details { get; set; }
    }
}
