using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PMSDAL;

namespace PMSDALIntializer
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var dc = new PMSDbContext())
            {
                Console.WriteLine(dc.Users.Count());
            }

            Console.Read();
        }
    }
}
