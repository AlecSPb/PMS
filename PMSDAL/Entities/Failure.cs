using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMSDAL.Entities
{
    /// <summary>
    /// 失败的靶材
    /// </summary>
    public class Failure
    {
        public Guid ID { get; set; }
        public DateTime CreateTime { get; set; }
        public string Creator { get; set; }

        public string State { get; set; }
        
        public string ProductID { get; set; }
        public string Composition { get; set; }
        public string PMINumber { get; set; }
        
        public string Stage { get; set; }
        public string Problem { get; set; }
        public string Process { get; set; }

        public string Remark { get; set; }
    }
}
