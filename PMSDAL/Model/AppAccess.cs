using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMSDAL.Model
{
    public class AppAccess
    {
        public Guid Id { get; set; }
        public string AccessDescrible { get; set; }//权限描述
        public string AccessCode { get; set; }//权限代码
    }
}
