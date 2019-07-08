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

namespace PMSClient.Express
{
    /// <summary>
    /// 专门访问顺丰的接口
    /// 秦雪梅13808071935
    /// </summary>
    public class SF
    {
        private string clientCode = "ZXS_5uJOL";
        private string checkWord = "BpbhlYBX5xal4wqTp1DdBPwQubuqty4c";
        private string custid = "7551234567 ";

        public void SFOrder()
        {
            string xml = @"<Request service='RouteService' lang='zh-CN'>
                           <Head>" + clientCode + @"</Head>
                                <Body>
                                    <RouteRequest
                                    tracking_type='1'
                                    method_type='1'
                                    tracking_number='306975876177'
                                    check_phoneNo='1935'/>
                                </Body>
                           </Request>";
            string verifyCode = MD5ToBase64String(xml + checkWord);
            //测试环境地址
            //string requestUrl = "https://bsp-ois.sit.sf-express.com:9443/bsp-ois/sfexpressService";
            string requestUrl = "http://bsp-oisp.sf-express.com/bsp-oisp/sfexpressService";
            //开发环境地址http://bsp-oisp.sf-express.com/bsp-oisp/sfexpressService 
            string result = DoPost(requestUrl, xml, verifyCode);//这就得到了返回结果，解析部分就不记了，想起来也没什么小点了
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
        public string DoPost(string Url, string xml, string verifyCode)
        {

            ServicePointManager.ServerCertificateValidationCallback =
                new System.Net.Security.RemoteCertificateValidationCallback(CheckValidationResult);
            string postData = string.Format("xml={0}&verifyCode={1}", xml, verifyCode);

            //请求
            WebRequest request = (HttpWebRequest)WebRequest.Create(Url);

            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded;charset=utf-8";
            request.ContentLength = Encoding.UTF8.GetByteCount(postData);
            byte[] postByte = Encoding.UTF8.GetBytes(postData);
            Stream reqStream = request.GetRequestStream();
            reqStream.Write(postByte, 0, postByte.Length);
            reqStream.Close();

            //读取
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream myResponseStream = response.GetResponseStream();
            StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"));
            string retString = myStreamReader.ReadToEnd();
            myStreamReader.Close();
            myResponseStream.Close();

            return retString;
        }



    }
}
