using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace PMSClient.Components.LaserNeed
{
    /// <summary>
    /// 原材料订单关于提供原料部分的处理程序
    /// </summary>
    public class LaserNeedHelper
    {


        public static List<LaserNeedModel> StrToLaserNeed(string str)
        {
            List<LaserNeedModel> laserInfos = new List<LaserNeedModel>();
            try
            {
                string[] pairs = str.Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries);
                foreach (var item in pairs)
                {
                    string[] pair = item.Split(new string[] { "+" }, StringSplitOptions.RemoveEmptyEntries);

                    if (pair.Length >= 4)
                    {
                        LaserNeedModel sm = new LaserNeedModel();
                        sm.LaserType = pair[0];
                        sm.Side = pair[1];
                        sm.Position = pair[2];
                        sm.Content = pair[3];
                        laserInfos.Add(sm);
                    }
                }
            }
            catch (Exception ex)
            {
                PMSHelper.CurrentLog.Error(ex);
            }

            return laserInfos;
        }


        public static string LaserNeedToStr(List<LaserNeedModel> laserInfos)
        {
            StringBuilder sb = new StringBuilder();
            if (laserInfos != null)
            {
                foreach (var item in laserInfos)
                {
                    if (!string.IsNullOrEmpty(item.LaserType))
                    {
                        sb.Append(item.LaserType);
                        sb.Append("+");
                        sb.Append(item.Side);
                        sb.Append("+");
                        sb.Append(item.Position);
                        sb.Append("+");
                        sb.Append(item.Content);
                        sb.Append(";");
                    }
                }
            }

            return sb.ToString();
        }


    }
}
