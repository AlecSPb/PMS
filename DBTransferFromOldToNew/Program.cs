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
            transfer.TransferOrder();





            Console.WriteLine("Data Transfer Complete");
            Console.Read();
        }
    }
}
