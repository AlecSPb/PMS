using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PMSClient.BasicService;

namespace PMSClient.ViewModel.Model
{
    public class CustomerExtra
    {
        public DcBDCustomer Customer { get; set; }
        public DateTime LastOrderDate { get; set; }
    }
}
