using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PMSClient.ReportsHelperNew;
using PMSClient.Express;
using System.IO;
using PMSClient.Components.EOrder.Midsummer;

namespace ConsoleTest
{

    public class Test
    {

        public void TestMidSummerXml()
        {
            var s = new MidXmlProcessor();
            var result = s.Anlysis("Examples\\MidSummerOrder27376.xml");

            //Console.WriteLine(s.ModelToString(result));

            Console.WriteLine(s.GetGoodOrder(result));
        }

        public void TestKDBird()
        {
            var api = new SF();
            //var req = new Request("", "1935", Shipper.SF, "SF1093354712812");
            //ZTO+75384842803543
            //var req = new Request("", "HTKY", "557024789905228");
            api.SenderPhone = "13808071935";
            string json = api.SFOrder("SF1093354712812");
            Console.WriteLine(json);
        }



        //public void TestFTPImage()
        //{
        //    var service = new ImageService();
        //    ImageFoundResult result = service.FindBondingImage("200321-AA-2");
        //    Console.WriteLine(result.IsFound);
        //    Console.WriteLine(result.ImagePath);
        //    Console.WriteLine(result.InfoMessage);
        //}



        public void TestCompositionHelper_Desend()
        {
            string composition = "Cu23.72In19.76Ga8.32Se48.20";

            string result = PMSClient.Helpers.CompositionHelper.ConvertToAtmDescend(composition);
            Console.WriteLine(result);
        }


        public void TestCompositionSimulator()
        {
            string input_str = @"100
Te+30
In+30
Ge+20
Se+20";
            var service = new PMSClient.Simulator.CompositionSimulatorService(0.5);
            string output_str = service.Simulate(input_str);
            Console.WriteLine(output_str);

            File.WriteAllText("data.csv", output_str);
            System.Diagnostics.Process.Start("data.csv");

        }



        public void TestSF()
        {
            string express_number = "265976836784";
            string phone = "15223459638";
            //string phone = "13808071935";

            var sf = new SF();
            sf.SenderPhone = phone;
            string result = sf.SFOrder(express_number);


            Console.WriteLine(result);

        }

        public void TestReportDataProcessHelper()
        {
            ReportDataProcessHelper helper = new ReportDataProcessHelper();
            string testdata = @"No.,Te atm%,Ge atm%,Sb atm%
1,55.64,21.86,22.51
2,56.08,22.14,21.77
3,55.95,21.90,22.15
4,55.98,22.38,21.64
5,55.67,22.47,21.86
6,55.88,22.53,21.59
7,55.97,21.75,22.28
8,55.26,21.72,23.01
9,55.70,22.55,21.75
10,55.15,22.55,22.30
11,55.42,21.90,22.67
12,55.88,22.18,21.95
13,55.45,22.33,22.21
Average,55.70,22.18,22.13";

            string str = helper.AppendStdDev(testdata);
            Console.WriteLine(str);
        }
    }
}
