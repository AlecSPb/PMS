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
                #region 数据
                data.Add("无需样品");
                data.Add("需要65gX1,25gx2");
                data.Add("需要65gX1,25gx3");
                data.Add("需要1cm大小x2");
                #endregion
                return data;
            }
        }

        public static List<string> Purity
        {
            get
            {
                var data = new List<string>();
                #region 数据
                data.Add("3N");
                data.Add("4N");
                data.Add("5N");
                data.Add("6N");
                #endregion
                return data;
            }
        }
        public static List<string> GrainSize
        {
            get
            {
                var data = new List<string>();
                #region 数据
                data.Add("-80");
                data.Add("-100");
                data.Add("-200");
                data.Add("-300");
                data.Add("-400");
                #endregion
                return data;
            }
        }
        public static List<double> MoldDiameter
        {
            get
            {
                var data = new List<double>();
                #region 数据
                data.Add(80);
                data.Add(125);
                data.Add(155);
                data.Add(205);
                data.Add(206);
                data.Add(233);
                data.Add(303);
                data.Add(455);
                #endregion
                return data;
            }
        }

    }
}
