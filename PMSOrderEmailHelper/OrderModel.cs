using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMSOrderEmailHelper
{
    public class OrderModel
    {
        public string Customer { get; set; }
        public string PO { get; set; }
        public DateTime PODate { get; set; }
        public double Quantity { get; set; }
        public string Purity { get; set; }
        public string Composition { get; set; }
        public string Drawing { get; set; }
        public string Diameter { get; set; }
        public string Thickness { get; set; }
        public string Tolerance { get; set; }
        public string Radius { get; set; }
        public string Roughness { get; set; }
        public string SampleForCustomer { get; set; }
        public string SampleForAnalysis { get; set; }
        public string BackingPlate { get; set; }
        public string BondingRequirement { get; set; }
        public DateTime ShipDate { get; set; }
    }
}
