using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMSClient.Helpers
{
    public static class VHPHelper
    {
        public static string CheckPlanTypeAndProcessCode(string planType, string processCode)
        {

            switch (processCode)
            {
                case "W1":
                    if (planType == PMSCommon.VHPPlanType.回收.ToString())
                    {
                        return "";
                    }
                    else
                    {
                        return "工艺代码[W1]通常对应的后续工作是[回收]";
                    }
                case "W2":
                case "W3":
                case "F1":
                    if (planType == PMSCommon.VHPPlanType.加工.ToString())
                    {
                        return "";
                    }
                    else
                    {
                        return "工艺代码[W2 W3 F1]通常对应的后续工作是[加工]";
                    }
                default:
                    return "";
            }

        }
    }
}
