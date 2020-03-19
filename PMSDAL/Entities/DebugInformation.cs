using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMSDAL
{
    public class DebugInformation
    {
        public Guid ID { get; set; }
        public DateTime CreateTime { get; set; }
        public string Information { get; set; }
    }
}
