using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMSClient.CheckLogic
{
    public static class OrderCheckLogic
    {
        public static CheckResult OutSourceCheck(string[] check_string)
        {
            var msg = new CheckResult();
            if (check_string == null)
            {
                msg.IsCheckOK = false;
                return msg;
            }

            foreach (var check_str in check_string)
            {
                if(string.IsNullOrEmpty(check_str))
                {
                    continue;
                }
                if (check_str.Contains("外包")||check_str.Contains("外购"))
                {
                    msg.IsCheckOK = true;
                    msg.Message = "外购项目，请在随后的核验中将【策略类型】设置为 【外包】\r\n如此，生产部门将不会看到该订单";
                    break;
                }

            }
            return msg;
        }

    }
}
