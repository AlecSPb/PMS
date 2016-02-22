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
            var query = from mo in service.GetAllMainOrders()
                        where mo.CustomerName.Contains("Midsummer")
                        select mo;
            foreach (var item in query.Take(10))
            {
                Console.WriteLine(item.CustomerName+" "+item.ProductName);
            }
        }
    }
}
