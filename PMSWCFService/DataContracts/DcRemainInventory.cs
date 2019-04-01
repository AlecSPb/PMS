using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PMSWCFService.DataContracts
{
    public class DcRemainInventory
    {
        public Guid ID { get; set; }
        public string Creator { get; set; }
        public DateTime CreateTime { get; set; }
        public string State { get; set; }

        public string ProductID { get; set; }
        public string Composition { get; set; }
        public string Dimension { get; set; }
        public string Details { get; set; }
    }
}