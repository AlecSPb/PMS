using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMSClient.Express.CheckHelper
{
    public class ExpressHelper
    {

        public ErrorMessage CheckSF(string number)
        {
            ErrorMessage message = new ErrorMessage();
            message.Item = number;


            if (!number.StartsWith("SF"))
            {
                if (number.Length != 12)
                {
                    message.Errors.Add("顺丰不以SF开头的长度单号应该为12位");
                }
            }
            else
            {
                if (number.Length != 15)
                {
                    message.Errors.Add("顺丰以SF开头的单号长度应该为15位");
                }
            }

            return message;
        }


        public ErrorMessage CheckUPS(string number)
        {
            ErrorMessage message = new ErrorMessage();
            message.Item = number;

            if (number.Length != 18)
            {
                message.Errors.Add("UPS单号长度为18位");

            }
            if (!number.StartsWith("1Z"))
            {
                message.Errors.Add("UPS单号通常以1Z开头");

            }
            return message;
        }


        public string ConcatErrorMessage(ErrorMessage message)
        {
            StringBuilder sb = new StringBuilder();
            if (message.Errors.Count > 0)
            {
                sb.AppendLine($"单号:[{ message.Item}]可能存在{message.Errors.Count}个错误,请注意核对");
                for (int i = 0; i < message.Errors.Count; i++)
                {
                    sb.AppendLine($"错误:{i + 1}-{message.Errors[i]}");
                }
            }
            return sb.ToString();
        }

    }
}
