using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PMSWCFService.Models
{
    public class AccessDc
    {
        public Guid ID { get; set; }
        public string AccessName { get; set; }
        public string AccessCode { get; set; }
        public int State { get; set; }
        public string ExtraInformation { get; set; }
    }
}