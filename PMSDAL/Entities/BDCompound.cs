using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMSDAL
{
    public class BDCompound
    {
        public Guid ID { get; set; }
        public string MaterialName { get; set; }
        public double Density { get; set; }
        public string MeltingPoint { get; set; }
        public string BoilingPoint { get; set; }
        public string SpecialProperty { get; set; }

        public string InformationSource { get; set; }

        public string Creator { get; set; }
        public DateTime CreateTime { get; set; }

    }
}
