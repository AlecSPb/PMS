using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMSDAL
{
    public class DeliveryAddress
    {
        public Guid ID { get; set; }
        public string Receiver { get; set; }
        public string Company { get; set; }
        public string Country { get; set; }
        public string Tax { get; set; }
        public string Phone { get; set; }
        public string PostCode { get; set; }
        public string Email { get; set; }
        public string CellPhone { get; set; }
        public string Address { get; set; }
    }
}
