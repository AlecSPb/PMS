using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace PMSWCFService.DataContracts
{
    [DataContract]
    public class DcMaterialOrderItemExtra
    {
        [DataMember]
        public DcMaterialOrder MaterialOrder { get; set; }
        [DataMember]
        public DcMaterialOrderItem MaterialOrderItem { get; set; }
    }
}