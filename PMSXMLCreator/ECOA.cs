using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMSXMLCreator
{
    public class ECOA
    {
        public Guid ID { get; set; }

        public string ProductID { get; set; }
        public string ProductName { get; set; }
        public string PONumber { get; set; }

        public string DeliveryTo { get; set; }
        public DateTime ScheduledShipDate { get; set; }
        public DateTime ActualShipDate { get; set; }

        //Material Parameters
        public double Weight { get; set; }
        public double Density { get; set; }
        public string Resistance { get; set; }
        public string ActualDimension { get; set; }

        public string XRF { get; set; }

        //GDMS
        public string GDMS { get; set; }

    }
}
