using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMSDAL
{
    /// <summary>
    /// 此类用于管理 装料工具箱
    /// </summary>
    public class ToolMilling
    {
        public Guid Id { get; set; }
        public DateTime CreateTime { get; set; }
        public string Creator { get; set; }

        public string State { get; set; }


        public int ToolNumber { get; set; }
        public int BoxNumber { get; set; }
        public string CompositionAbbr { get; set; }

        public string Remark { get; set; }
    }
}
