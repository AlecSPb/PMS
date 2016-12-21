using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    /// <summary>
    /// 热压过程当中的记录
    /// </summary>
    public class RecordVHP
    {
        public Guid ID { get; set; }
        public string CreateTime { get; set; }
        public string Creator { get; set; }
        public string Remark { get; set; }
    }
}
