using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMSDAL
{
    /// <summary>
    /// 失败的靶材
    /// </summary>
    public class Failure
    {
        [Key]
        public Guid ID { get; set; }
        public DateTime CreateTime { get; set; }
        public string Creator { get; set; }

        public string State { get; set; }
        
        public string ProductID { get; set; }
        //所有的其他信息全部写入Details
        public string Details { get; set; }
        
        public string Stage { get; set; }
        public string Problem { get; set; }
        public string Process { get; set; }

        public string Remark { get; set; }
    }
}
