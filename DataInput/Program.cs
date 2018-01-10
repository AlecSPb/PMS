using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataInput
{
    class Program
    {
        static void Main(string[] args)
        {
            CompoundBatchOperate op = new CompoundBatchOperate();
            op.ReadAll();
            Console.WriteLine("执行完毕");



            Console.Read();
        }
    }
}
