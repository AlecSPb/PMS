using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMSDAL
{
    public class BDVHPMold
    {
        public Guid ID { get; set; }
        public string MoldType { get; set; }
        public string MoldDetails { get; set; }
        public double InnerDiameter { get; set; }
        public double ModelHeight { get; set; }
        public string State { get; set; }
        public string Creator { get; set; }
        public DateTime CreateTime { get; set; }

    }

}
