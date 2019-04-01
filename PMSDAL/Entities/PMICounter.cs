using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMSDAL
{
    /// <summary>
    /// 对日常的背板等物品进行统计
    /// </summary>
    public class PMICounter:ModelBase
    {
        public string ItemGroup { get; set; }
        public string ItemName { get; set; }
        public string ItemSpecification { get; set; }
        public string ItemDetails { get; set; }
        public double ItemCount { get; set; }
        public string Unit { get; set; }

    }
}
