using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PMSDAL;
using PMSWCFService.DataContracts;
using System.Runtime.Serialization;

namespace PMSWCFService.DataContracts
{
    [DataContract]
    public class DcDeliveryItemExtra
    {
        [DataMember]
        public DcDeliveryItem DeliveryItem { get; set; }
        [DataMember]
        public DcDelivery Delivery { get; set; }
    }
}