using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMSDAL
{
    public class ToolFilling
    {
        public Guid Id { get; set; }
        public DateTime CreateTime { get; set; }
        public string Creator { get; set; }

        public string State { get; set; }


        public int ToolNumber { get; set; }
        public string CompositionAbbr { get; set; }

        public string Remark { get; set; }
    }
}
