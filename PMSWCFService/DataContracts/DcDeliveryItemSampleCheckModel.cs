using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PMSWCFService.DataContracts
{
    public class DcDeliveryItemSampleCheckModel
    {
        public DateTime DeliveryTime { get; set; }
        public string ProductID { get; set; }
        public string Composition { get; set; }
        public string Customer { get; set; }
        public string PMINumber { get; set; }

        public string SampleInformation { get; set; }
        public string SampleDeliveryInformation { get; set; }
    }
}