using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMSDAL
{
    /// <summary>
    /// 客户信息
    /// </summary>
    public class BDCustomer
    {
        public Guid ID { get; set; }
        public string CustomerName { get; set; }
        public string Address { get; set; }
        public string ContactPerson { get; set; }
        public string Phone1 { get; set; }
        public string Phone2 { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
        public string Memo { get; set; }
        public string State { get; set; }
        public int ImportanceLevel { get; set; }
    }
}
