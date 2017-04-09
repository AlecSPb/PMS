using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PMSClient.MainService;

namespace UsefulPackage
{
    public static class PMTranslate
    {
        /// <summary>
        /// 生成热压靶材的Lot号码
        /// </summary>
        public static string VHPPlanLot()
        {
            return DateTime.Now.ToString("yyMMdd") + "M" + 1;
        }
        public static string VHPPlanLot(DcPlanVHP plan, string suffix)
        {
            if (plan==null)
            {
                return VHPPlanLot();
            }
            string dateStr = plan.PlanDate.ToString("yyMMdd");
            string deviceStr = plan.VHPDeviceCode;
            return $"{dateStr}-{deviceStr}-{suffix}";
        }
        public static string VHPPlanLot(DateTime date, string devicecode, string suffix)
        {
            string dateStr = date.ToString("yyMMdd");
            string deviceStr = devicecode;
            return $"{dateStr}-{deviceStr}-{suffix}";
        }

    }
}
