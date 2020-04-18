using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PMSWCFService.DataContracts
{
    public class DcPlanTrace
    {
        public Guid ID { get; set; }
        public string SearchCode { get; set; }
        public DateTime PlanDate { get; set; }
        public string PlanType { get; set; }
        public string VHPDeviceCode { get; set; }
        public string CompositionStd { get; set; }
        public double MoldDiameter { get; set; }
        public int Quantity { get; set; }

        public string Customer { get; set; }
        public string PMINumber { get; set; }
        public string Dimension{ get; set; }

        public double OrderQuantity { get; set; }
        //存储查询结果
        public string RecordDeMold { get; set; }
        public string RecordMachine { get; set; }
        public string RecordTest { get; set; }
        public string RecordBonding { get; set; }
        public string RecordDelivery { get; set; }
        public string RecordFailure { get; set; }

    }
}