using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocGenerator.DocModels
{
    public class Product
    {
        public Guid ID { get; set; }
        public string ProductID { get; set; }
        public string Composition { get; set; }
        public string CompositionAbbr { get; set; }
        public string PO { get; set; }
        public string Customer { get; set; }
        public string Dimension { get; set; }
        public string Density { get; set; }
        public string Weight { get; set; }
        public string Resistance { get; set; }
        public string CompositionXRF { get; set; }
        public string DimensionActual { get; set; }
        public string Remark { get; set; }
        public DateTime CreateTime { get; set; }
        public string Creator { get; set; }
    }
}
