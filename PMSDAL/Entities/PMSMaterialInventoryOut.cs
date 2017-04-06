using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMSDAL
{
    public class PMSMaterialInventoryOut
    {
        public Guid Id { get; set; }
        public string State { get; set; }
        public string Creator { get; set; }
        public DateTime CreateTime { get; set; }

        public string Composition { get; set; }
        public string Purity { get; set; }
        public string Supplier { get; set; }
        public double Weight { get; set; }
        public string Remark { get; set; }
    }
}
