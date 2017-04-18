using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PMSDAL;
namespace PMSWCFService.DataContracts
{
    public class DcDeliveryItemExtra
    {
        public DeliveryItem DeliveryItem { get; set; }
        public Delivery Delivery { get; set; }
    }
}