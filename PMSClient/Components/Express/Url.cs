using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMSClient.Express
{
    public static class Url
    {
        public static string FormalUrl { get; set; } 
            = @"http://api.kdniao.com/Ebusiness/EbusinessOrderHandle.aspx";
        public static string TestUrl { get; set; } 
            = @"http://sandboxapi.kdniao.com:8080/kdniaosandbox/gateway/exterfaceInvoke.json";
    }
}
