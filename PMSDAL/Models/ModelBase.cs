using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMSDAL
{
    /// <summary>
    /// 所有模型的基类
    /// </summary>
    public class ModelBase
    {
        public Guid ID { get; set; }
        public string Creator { get; set; }
        public int State { get; set; }
        public DateTime CreateTime { get; set; }
    }
}
