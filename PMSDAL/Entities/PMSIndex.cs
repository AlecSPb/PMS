using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMSDAL
{
    public class PMSIndex
    {
        public Guid ID { get; set; }
        public DateTime CreateTime { get; set; }
        public string IndexType { get; set; }
        public double IndexValue { get; set; }
    }
}
