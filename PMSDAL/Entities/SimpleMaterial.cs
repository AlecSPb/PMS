using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMSDAL
{
    public class SimpleMaterial : ModelBase
    {
        public string ElementName { get; set; }
        public double UnitPrice { get; set; }
        public string Remark { get; set; }
    }
}
