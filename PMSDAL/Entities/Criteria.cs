using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMSDAL.Entities
{
    /// <summary>
    /// 发货标准
    /// </summary>
    public class Criteria : ModelBase
    {
        public string SearchCode { get; set; }
        public string Category { get; set; }
        public string Content { get; set; }

        public DateTime EffectTime { get; set; }

        public DateTime AbolishTime { get; set; }

        public string PreviousSearchCode { get; set; }
    }
}
