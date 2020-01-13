using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CScanImageFinder
{
    class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine("请先将要查询的所有产品ID填入本程序目录下的[TargetIDCollection.txt]文件内，然后继续");
            Console.Read();

            ImageFound finder = new ImageFound();
            finder.RunSearch("TargetIDCollection.txt");
            Console.WriteLine("Done");
            Console.Read();
        }
    }
}
