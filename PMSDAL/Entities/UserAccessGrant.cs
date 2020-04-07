using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMSDAL
{
    public class UserAccessGrant
    {
        public Guid ID { get; set; }
        public string ControlName { get; set; }
        public string RoleGroupString { get; set; }
        public string Remark { get; set; }
    }
}
