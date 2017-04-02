using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMSDAL
{
    /// <summary>
    /// 记录服务端错误
    /// </summary>
    public class LogError
    {
        public Guid ID { get; set; }
        public DateTime CreateTime { get; set; }
        public string Error { get; set; }
    }
}
