using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMSClient.Express.CheckHelper
{
    public class ExpressHelper
    {

        public ErrorMessage  CheckSF(string number)
        {
            ErrorMessage message = new ErrorMessage();
            message.Item = $"顺丰单号:{number}可能存在错误";
            if (number.Length != 12)
            {
                message.Errors.Add("顺丰单号长度为12位");
            }
            return message;
        }


        public ErrorMessage CheckUPS(string number)
        {
            ErrorMessage message = new ErrorMessage();
            message.Item = $"UPS单号:{number}可能存在错误";
            if (number.Length != 16)
            {
                message.Errors.Add("UPS单号长度为12位");

            }
            if (number.StartsWith("1Z"))
            {
                message.Errors.Add("UPS单号通常以1Z开头");

            }
            return message;
        }


        public string ConcatErrorMessage(ErrorMessage message)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(message.Item);
            foreach (var error in message.Errors)
            {
                sb.AppendLine(error);
            }
            return sb.ToString();
        }

    }
}
