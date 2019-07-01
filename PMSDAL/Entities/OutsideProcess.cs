using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMSDAL
{
    /// <summary>
    /// 此模块是因为外协加工管理混乱所采取的解决方案
    /// </summary>
    public class OutsideProcess : ModelBase
    {
        public string ProductID { get; set; }
        public string Composition { get; set; }
        public string Dimension { get; set; }
        public string PMINumber { get; set; }
        public string PONumber { get; set; }
        public string Customer { get; set; }

        public string Processor { get; set; }    
        public string ProgressBar { get; set; }
        public string Remark { get; set; }
    }
}
