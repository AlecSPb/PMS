using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMSQuotation.Models
{
    public class Contacts
    {
        public int ID { get; set; }
        public string CustomerType { get; set; }//self,customer
        public string CompanyName { get; set; }
        public string Address { get; set; }
        public string PostCode { get; set; }
        public string Contact { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Fax { get; set; }
        public string State { get; set; }



    }
}
