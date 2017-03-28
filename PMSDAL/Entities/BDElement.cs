using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMSDAL
{
    /// <summary>
    /// 存储元素周期表
    /// </summary>
    public class BDElement
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public int AtomicNumber { get; set; }
        public double MolWeight { get; set; }
        public double Density { get; set; }
        public string MeltingPoint { get; set; }
        public string BoilingPoint { get; set; }
    }
}
