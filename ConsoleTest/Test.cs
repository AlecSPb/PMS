using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PMSClient.ReportsHelperNew;

namespace ConsoleTest
{
    public class Test
    {
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

            helper.AppendStdDev(testdata);

        }
    }
}
