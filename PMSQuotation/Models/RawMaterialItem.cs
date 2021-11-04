using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMSQuotation.Models
{
    public class RawMaterialItem
    {
        public string Material { get; set; }
        public double UnitPrice { get; set; }
        public double Weight { get; set; }
    }
}
