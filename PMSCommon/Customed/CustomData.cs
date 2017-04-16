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

        public static List<string> Quantity
        {
            get
            {
                var data = new List<string>();
                #region 数据
                for (int i = 1; i < 10; i++)
                {
                    data.Add(i.ToString());
                }
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


    }
}
