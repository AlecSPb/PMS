using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace PMSClient.CheckLogic
{
    public static class OrderCheckLogic
    {
        public static CheckResult OutSourceCheck(string[] check_string)
        {
            var msg = new CheckResult();
            msg.IsCheckOK = true;
            if (check_string == null)
            {
                msg.IsCheckOK = true;
                return msg;
            }

            foreach (var check_str in check_string)
            {
                if (string.IsNullOrEmpty(check_str))
                {
                    continue;
                }
                if (check_str.Contains("外包") || check_str.Contains("外购"))
                {
                    msg.IsCheckOK = false;
                    msg.Message = "外购项目，请在随后的核验中将【策略类型】设置为 【外包】\r\n如此，生产部门将不会看到该订单";
                    break;
                }

            }
            return msg;
        }


        public static bool CheckPMINumber(string pminumber)
        {
            var regex = new Regex(@"^CD\d{6}-\w{1}$");
            var checkresult = regex.Match(pminumber);
            return checkresult.Success;
        }

    }
}
