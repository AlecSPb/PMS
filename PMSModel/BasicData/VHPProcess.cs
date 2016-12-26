using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMSModel
{
    /// <summary>
    /// 热压工艺表
    /// </summary>
    public class VHPProcess
    {
        public Guid ID { get; set; }
        public string CodeName { get; set; }
        public string CodeMeaning { get; set; }
        public string Creator { get; set; }
        public DateTime CreateTime { get; set; }
    }
}
