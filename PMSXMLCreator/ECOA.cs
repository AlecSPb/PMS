using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMSXMLCreator
{
    public class ECOA
    {
        public static ECOA NewInstance()
        {
            ECOA model = new ECOA
            {
                ID = Guid.NewGuid(),
                ProductID = $"{DateTime.Today.ToString("yyMMdd")}-C-1",
                ProductName = "Se44As33Ge22",
                PONumber = "123456",
                DeliveryTo = "TCB",
                ScheduledShipDate = DateTime.Today.AddDays(10),
                ActualShipDate = DateTime.Today.AddDays(12),

                Weight = "5040",
                Density = "4.3",
                Resistance = "OutOfRange",
                ActualDimension = "440mm ODx 7.8mm thickness",

                Composition = "none",
                GDMS = "Li=30 B=40 P=56 F=23"
            };

            return model;
        }
        public Guid ID { get; set; }

        public string ProductID { get; set; }
        public string ProductName { get; set; }
        public string PONumber { get; set; }

        public string DeliveryTo { get; set; }
        public DateTime ScheduledShipDate { get; set; }
        public DateTime ActualShipDate { get; set; }

        //Material Parameters
        public string Weight { get; set; }
        public string Density { get; set; }
        public string Resistance { get; set; }
        public string ActualDimension { get; set; }

        public string Composition { get; set; }

        //GDMS
        public string GDMS { get; set; }

    }
}
