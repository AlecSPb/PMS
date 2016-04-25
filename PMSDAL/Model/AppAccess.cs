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
        public string AccessName { get; set; }
        public string AccessCode { get; set; }
    }
}
