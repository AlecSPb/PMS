using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBTransferFromOldToNew
{
    class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine("Start Data Transfering");
            var transfer = new DBTransfer();

            //注意选择自己所需的，小心操作

            //transfer.TransferOrderPlan();
            //transfer.TransferDensity();

            transfer.Product();

            Console.WriteLine("Data Transfer Complete");
            Console.Read();
        }
    }
}
