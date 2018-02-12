using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using DataOutputTempProgram.DataService;

namespace DataOutputTempProgram
{
    class DataProcess
    {
        public void Process()
        {
            string fileread = Path.Combine(Environment.CurrentDirectory, "产品和制粉记录No.csv");
            string filewrite = Path.Combine(Environment.CurrentDirectory, "产品和制粉记录Write.csv");
            StreamReader sr = new StreamReader(fileread, System.Text.Encoding.GetEncoding("GB2312"));
            StreamWriter sw = new StreamWriter(new FileStream(filewrite, FileMode.Create), System.Text.Encoding.GetEncoding("GB2312"));
            string line = sr.ReadLine();
            using (var service = new MissonServiceClient())
            {
                while ((line = sr.ReadLine()) != null)
                {
                    string searchCode = Helper.Process(line);
                    DcPlanWithMisson vhp = service.GetPlanExtra(0, 10, searchCode, "").FirstOrDefault();
                    if (vhp != null)
                    {
                        line += "," + vhp.Plan.Temperature + "," + vhp.Plan.Pressure + "," + vhp.Plan.KeepTempTime;
                    }
                    else
                    {
                        line += ",,,";
                    }
                    Console.WriteLine(line.Substring(0, 10));
                    sw.WriteLine(line);
                }
                sr.Close();
                sw.Close();
            }

            Console.WriteLine("处理完毕");
        }
    }
}
