using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WcfService;

namespace TempTest
{
    class TestWCF
    {
        private MainOrderService service;
        public TestWCF()
        {
            service = new MainOrderService();
        }
        public void GetAllMainOrders()
        {
            service.GetAllMainOrders().ForEach(mo=>Console.WriteLine(mo.ProductName));
        }
    }
}
