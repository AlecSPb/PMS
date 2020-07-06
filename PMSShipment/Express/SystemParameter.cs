using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMSShipment.Express
{
    public class SystemParameter
    {
        public SystemParameter()
        {
            //即时查询接口
            RequestType = "1002";
        }

        //请求内容
        public string RequestData { get; set; }
        //用户id
        public string EBusinessID { get; set; }
        //请求接口指令
        public string RequestType { get; set; }
        /// <summary>
        /// 数据内容签名
        /// 把(请求内容(未编码)+ApiKey)进行 MD5 加密， 然后 Base64编码， 最后进行 URL(utf-8)编码
        /// </summary>
        public string DataSign { get; set; }
        //DataType=2,请求返回都用json
        public string DataType { get; set; }

    }
}
