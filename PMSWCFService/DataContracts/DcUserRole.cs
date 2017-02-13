using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PMSWCFService.DataContracts
{
    public class DcUserRole
    {
        public Guid ID { get; set; }
        public string GroupName { get; set; }
        public string ExtraInformation { get; set; }
        public string State { get; set; }
        public DateTime CreateTime { get; set; }
    }
}