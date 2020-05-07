using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PMSWCFService.DataContracts
{
    public class DcAccessGrant
    {
        public string ControlName { get; set; }
        public string RoleGroupString { get; set; }
    }
}