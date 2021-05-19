using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMSSPC.Models
{
    public class RecordTestModel
    {
        public Guid ID { get; set; }
        public string ProductID { get; set; }
        public string Composition { get; set; }
        public double Density { get; set; }
        public string Weight { get; set; }
        public string Resistance { get; set; }
        public string DimensionActual { get; set; }
        public DateTime CreateTime { get; set; }
    }
}
