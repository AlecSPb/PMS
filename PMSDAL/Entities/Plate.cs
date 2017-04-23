using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMSDAL
{
    public class Plate
    {
        public Guid ID { get; set; }
        public string Creator { get; set; }
        public DateTime CreateTime { get; set; }
        public string State { get; set; }
        public string PlateLot { get; set; }
        public string Dimension { get; set; }
        public string Weight { get; set; }
        public string Supplier { get; set; }
        public string Appearance { get; set; }
        public string Defects { get; set; }
        public string Remark { get; set; }
    }
}
