using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotificationCommon
{
    public class Notice
    {
        public Guid ID { get; set; }
        public DateTime CreateTime { get; set; }
        public string Creator { get; set; }
        public DateTime StartTime { get; set; }//通知开始时间，默认是Now
        public DateTime EndTime { get; set; }//通知停止时间，默认是开始时间+1天
        public string Type { get; set; }
        public string Content { get; set; }
        public int Priority { get; set; }//1到5，默认是1，5为最高优先级
        public int State { get; set; }//1=有效，2=无效

    }
}
