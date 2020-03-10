using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMSDAL
{
    /// <summary>
    /// 供应商
    /// </summary>
    public class BDSupplier
    {
        public Guid ID { get; set; }
        public string SupplierName { get; set; }
        public string Abbr { get; set; }
        public string ContactPerson { get; set; }
        public string CellPhone { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }

        public string Remark { get; set; }
    }
}
