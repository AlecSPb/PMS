using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMSDAL
{
    /// <summary>
    /// 记录日志信息
    /// 暂时不在服务端增加客户端身份信息
    /// </summary>
    public class LogInformation
    {
        public Guid ID { get; set; }
        public DateTime CreateTime { get; set; }
        public string Log { get; set; }
    }
}
