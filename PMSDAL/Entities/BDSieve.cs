using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMSDAL
{
    /// <summary>
    /// 筛子
    /// </summary>
    public class BDSieve
    {
        public Guid ID { get; set; }
        public string SieveName { get; set; }
        public string Mesh { get; set; }
        public DateTime StartTime { get; set; }
        public string Manufacuture { get; set; }
        public int EstimateUsedCount { get; set; }
        public int CurrentCount { get; set; }

        public string State { get; set; }
        public string Creator { get; set; }
        public DateTime CreateTime { get; set; }
    }
}
