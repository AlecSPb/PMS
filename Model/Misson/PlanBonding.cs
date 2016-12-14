using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Database
{
    /// <summary>
    /// 绑定计划
    /// </summary>
    public class Bonding
    {
        public Guid ID { get; set; }
        public Guid OrderID { get; set; }
        public string DimensionCheck { get; set; }
        public string WarpCheck { get; set; }
        public string BackplateCheck { get; set; }
        public string TargetPreProcessCheck { get; set; }
        public string BackPlatePreProcessCheck { get; set; }
        public string BondingRecord { get; set; }
        public string AfterCheck { get; set; }
        public string Remark { get; set; }
    }
}
