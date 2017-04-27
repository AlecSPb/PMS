using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PMSDAL;

namespace PMSWCFService.DataContracts
{
    public class PMSMaterialOrderItemExtra
    {
        public MaterialOrder MaterialOrder { get; set; }
        public MaterialOrderItem MaterialOrderItem { get; set; }
    }
}