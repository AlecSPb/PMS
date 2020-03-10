using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMSDAL
{
    /// <summary>
    /// 三杰记录初级原料的库存，用作追踪初级原料的数据表
    /// </summary>
    public class RawMaterialSheet : ModelBase
    {
        public string Lot { get; set; }
        public string Supplier { get; set; }
        public double Weight { get; set; }//kg
        public string Remark { get; set; }
        public DateTime StoreTime { get; set; }
        //State 作废 [在库] 耗尽 取消
    }
}
