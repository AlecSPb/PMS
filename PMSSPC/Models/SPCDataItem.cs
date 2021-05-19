using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMSSPC.Models
{
    public class SPCDataItem
    {
        public string ProductID { get; set; }
        public string Composition { get; set; }
        public double Value { get; set; }//can be density ,diameter,height,weight,resistance
        public DateTime CreateTime { get; set; }
    }
}
