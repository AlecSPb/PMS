using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMSDAL
{
    /// <summary>
    /// 检查单历史记录
    /// </summary>
    public class CheckListHistory
    {
        public Guid ID { get; set; }
        public string Creator { get; set; }
        public DateTime CreateTime { get; set; }
        public string State { get; set; }

        public DateTime UpdateTime { get; set; }
        public string Updator { get; set; }

        public string Title { get; set; }
        public string Introduction { get; set; }
        public string Content { get; set; }

        //操作者和操作时间
        [Key]
        public Guid HistoryID { get; set; }
        public string Operator { get; set; }
        public DateTime OperateTime { get; set; }
    }
}
