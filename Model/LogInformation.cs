using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    /// <summary>
    /// 谁，什么时间，做了什么事情
    /// </summary>
    public class LogInformation
    {
        public Guid ID { get; set; }
        public string CurrentUser { get; set; }
        public DateTime LogTime { get; set; }
        public string LogData { get; set; }
    }
}
