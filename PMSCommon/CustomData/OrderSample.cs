using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMSCommon
{
    public static class CustomData
    {
        public static List<string> OrderSampleNeeds
        {
            get
            {
                var data = new List<string>();
                data.Add("需要65gX1,,25gx2");
                data.Add("需要65gX1,,25gx3");
                data.Add("需要1cm大小x2");
                return data;
            }
        }








    }
}
