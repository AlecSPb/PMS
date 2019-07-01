using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PMSWCFService.DataContracts
{
    public class DcOutsideProcess
    {
        public Guid ID { get; set; }
        public string Creator { get; set; }
        public DateTime CreateTime { get; set; }
        public string State { get; set; }

        public string ProductID { get; set; }
        public string Composition { get; set; }
        public string Dimension { get; set; }
        public string PMINumber { get; set; }
        public string PONumber { get; set; }
        public string Customer { get; set; }

        public string Processor { get; set; }
        public string ProgressBar { get; set; }
        public string Remark { get; set; }
    }
}