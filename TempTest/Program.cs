using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TempTest
{
    class Program
    {
        static void Main(string[] args)
        {
            TestWCF test = new TestWCF();
            //test.GetAllMainOrders();
            test.GetMainPlansByMainOrderId();
            Console.Read();
        }
    }
}
