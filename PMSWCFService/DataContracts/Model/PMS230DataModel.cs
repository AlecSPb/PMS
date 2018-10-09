using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace PMSWCFService.DataContracts
{
    [DataContract]
    public class PMS230DataModel
    {
        [DataMember]
        public DcRecordTest Test;
        [DataMember]
        public DcRecordBonding Bond;
        [DataMember]
        public DcDeliveryItem Delivery;

    }
}