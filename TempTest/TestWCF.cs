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
        private MainOrderService mainorderservice;
        private MainPlanService mainplanservice;
        public TestWCF()
        {
            mainorderservice = new MainOrderService();
            mainplanservice = new MainPlanService();
        }
        public void GetAllMainOrders()
        {
            var query = from mo in mainorderservice.GetAllMainOrders()
                        where mo.CustomerName.Contains("Midsummer")
                        select mo;
            foreach (var item in query.Take(10))
            {
                Console.WriteLine(item.CustomerName+" "+item.ProductName);
            }
        }
        public void GetMainPlansByMainOrderId()
        {
            var mainplans = mainplanservice.GetMainPlansByMainOrderId(new Guid("d3a51f48-8979-40f1-a786-956896805a47"));
            foreach (var item in mainplans)
            {
                Console.WriteLine(item.VHPTime.ToString());
            }
        }
    }
}
