using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Misson
{
    /// <summary>
    /// 维护计划
    /// </summary>
    public class PlanMaitenance
    {
        public Guid ID { get; set; }
        public DateTime CreateTime { get; set; }
        public string Creator { get; set; }
        public int State { get; set; }



        public string Remark { get; set; }
    }
}
