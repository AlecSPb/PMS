using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMSDAL
{
    public class EnvironmentInfo
    {
        public Guid ID { get; set; }
        public DateTime UpdateTime { get; set; }
        public double Temperature { get; set; }
        public double Humidity { get; set; }
    }
}
