using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMSModel
{
    /// <summary>
    /// 绑定计划
    /// </summary>
    public class PlanBonding
    {
        public Guid ID { get; set; }
        public DateTime CreateTime { get; set; }
        public string Creator{get; set; }
        public int State { get; set; }
        public DateTime BondingTime { get; set; }


        public string Remark { get; set; }
    }
}
