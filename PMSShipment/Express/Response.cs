using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMSShipment.Express
{
    public class Response
    {
        public string EBusinessID { get; set; }
        public string OrderCode { get; set; }
        public string ShipperCode { get; set; }
        public string LogisticCode { get; set; }
        public bool Success { get; set; }
        public string State { get; set; }
        public string Reason { get; set; }

        public Trace[] Traces { get; set; }
    }
}
