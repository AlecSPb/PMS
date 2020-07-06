using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMSShipment.Express
{
    public class Request
    {
        public Request()
        {

        }
        public Request(string ordercode, string shippercode, string logisticode)
        {
            OrderCode = ordercode;
            ShipperCode = shippercode;
            LogisticCode = logisticode;
        }
        public string OrderCode { get; set; }
        public string ShipperCode { get; set; }
        public string LogisticCode { get; set; }

    }
}
