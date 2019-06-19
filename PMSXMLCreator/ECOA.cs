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
            ECOA model = new ECOA();
            model.ID = Guid.NewGuid();
            model.ProductID = $"{DateTime.Today.ToString("yyMMdd")}-C-1";
            model.ProductName = "Se44As33Ge22";
            model.PONumber = "123456";
            model.DeliveryTo = "TCB";
            model.ScheduledShipDate = DateTime.Today.AddDays(10);
            model.ActualShipDate = DateTime.Today.AddDays(12);

            model.Weight = "5040";
            model.Density = "4.3";
            model.Resistance = "OutOfRange";
            model.ActualDimension = "440mm ODx 7.8mm thickness";

            model.Composition = "none";
            model.GDMS = "Li=30 B=40 P=56 F=23";

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
