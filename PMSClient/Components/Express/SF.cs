using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using System.Xml;

namespace PMSClient.Express
{
    /// <summary>
    /// 专门访问顺丰的接口
    /// 秦雪梅13808071935
    /// https://qiao.sf-express.com
    /// newlifechou+Newlifechou2012
    /// 月结0285930245
    /// 有限制，测试环境默认2000次/天，转生产环境后默认500000次/天。
    /// </summary>
    public class SF
    {
        public SF()
        {
            Sender = Properties.Settings.Default.ExpressSender;
            SenderPhone = Properties.Settings.Default.ExpressSenderPhone;
        }
        public string Sender { get; set; } = "秦雪梅";
        public string SenderPhone { get; set; } = "13808071935";

        public void WS()
        {
            using (var client = new PMSClient.SF.ServiceClient())
            {
                string xml = @"<Request service='RouteService' lang='zh-CN'>
                           <Head>" + clientCode + "," + checkWord + @"</Head>
                                <Body>
                                    <RouteRequest tracking_type='1' method_type='1' tracking_number='306975876140' check_phoneNo='1935'/>
                                </Body>
                           </Request>";

                string s = client.sfexpressService(xml);
            }
        }

        //private string clientCode = "ZXS_5uJOL";
        //private string checkWord = "BpbhlYBX5xal4wqTp1DdBPwQubuqty4c";

        //利用丰桥里的第三方TrackingMore 功能
        private string clientCode = "ZXS_TM";
        private string checkWord = "UxfmVRiKnTHjD3My8jUGduBJndgNvtwf";
        public string SFOrder(string trackingNumber)
        {
            string last4Digital = SenderPhone.Substring(SenderPhone.Length - 4, 4);
            string xml = @"<Request service='RouteService' lang='zh-CN'>
                           <Head>" + clientCode + @"</Head>
                                <Body>
                                    <RouteRequest tracking_type='1' method_type='1' tracking_number='306975876140' check_phoneNo=" + $"'{last4Digital}'" + @"/>
                                </Body>
                           </Request>";

            xml = xml.Replace("306975876140", trackingNumber);

            string verifyCode = MD5ToBase64String(xml + checkWord);
            string requestUrl = @"http://bsp-oisp.sf-express.com/bsp-oisp/sfexpressService";
            string xml_response = DoPost(requestUrl, xml, verifyCode);

            string format_str = ResolveXMLResponse(xml_response);
            return format_str;
        }

        public string ResolveXMLResponse(string xml)
        {
            string error_msg = "查询出错，请使用手动查询";
            StringBuilder sb = new StringBuilder();
            sb.Clear();
            XmlDocument document = new XmlDocument();
            document.LoadXml(xml);
            XmlNode head = document.SelectSingleNode(@"Response/Head");
            if (head == null) return error_msg;
            string head_value = head.InnerText;
            if (head_value != "OK")
            {
                sb.AppendLine(error_msg);
            }
            else
            {
                XmlNodeList routes = document.SelectNodes(@"Response/Body/RouteResponse/Route");
                foreach (XmlNode route in routes)
                {
                    string accept_address = route.Attributes["accept_address"].Value;
                    string accept_time = route.Attributes["accept_time"].Value;
                    string remark = route.Attributes["remark"].Value;
                    sb.AppendLine($"{accept_time}-{accept_address}-{remark}");
                }
            }

            return sb.ToString();
        }




        public string MD5ToBase64String(string str)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] MD5 = md5.ComputeHash(Encoding.UTF8.GetBytes(str));//MD5(注意UTF8编码)
            string result = Convert.ToBase64String(MD5);//Base64
            return result;
        }
        public bool CheckValidationResult(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
        {   // 总是接受  
            return true;
        }
        public string DoPost(string url, string xml, string verifyCode)
        {

            //ServicePointManager.ServerCertificateValidationCallback =
            //    new System.Net.Security.RemoteCertificateValidationCallback(CheckValidationResult);
            string postData = string.Format("xml={0}&verifyCode={1}", xml, verifyCode);

            //请求
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.ContentType = "application/x-www-form-urlencoded;charset=utf-8";
            request.Referer = url;
            request.Accept = "*/*";
            request.Timeout = 30 * 1000;
            request.UserAgent = "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.1; SV1; .NET CLR 2.0.50727; .NET CLR 3.0.04506.648; .NET CLR 3.0.4506.2152; .NET CLR 3.5.30729)";
            request.Method = "POST";
            request.ContentLength = Encoding.UTF8.GetByteCount(postData);
            byte[] postByte = Encoding.UTF8.GetBytes(postData);

            Stream reqStream = request.GetRequestStream();
            reqStream.Write(postByte, 0, postByte.Length);
            reqStream.Flush();
            reqStream.Close();

            //读取
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream myResponseStream = response.GetResponseStream();
            StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"));
            string retString = myStreamReader.ReadToEnd();
            myStreamReader.Close();
            myResponseStream.Close();
            response.Close();
            request.Abort();
            return retString;
        }



    }
}
