using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMSQuotation.Models
{
    public class BasicInfo
    {
        public Guid ID { get; set; }
        public string CompanyName { get; set; }
        public string Address { get; set; }
        public string PostCode { get; set; }
        public string Contact { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }

    }
}
