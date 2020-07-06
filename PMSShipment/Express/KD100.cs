using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace PMSShipment.Express
{
    public class KD100
    {
        private string customer = "6EBA9000AAABEC4F12E32A223AC309CA";
        private string key = "vnJevXCY9561";
        private string url = @"https://poll.kuaidi100.com/poll/query.do";
        /// <summary>
        /// json方式查询订单实时追踪
        /// </summary>
        /// <returns></returns>
        public string GetOrderTracesByJson()
        {
            Param param = new Param();
            param.com = "shunfeng";
            param.num = "306975876177";
            param.phone = "13808071935";
            param.from = "";
            param.to = "";
            param.resultv2 = "1";

            string param_data = JsonConvert.SerializeObject(param);

            Dictionary<string, string> req = new Dictionary<string, string>();
            req.Add("customer", customer);
            string sign = Helper.ToMD5(param_data + key + customer).ToUpper();
            req.Add("sign", sign);
            req.Add("param", param_data);

            string result = SendPost(url, req);

            //根据公司业务处理返回的信息......

            return result;
        }

        private string SendPost(string url, Dictionary<string, string> req)
        {
            string result = "";
            StringBuilder postData = new StringBuilder();
            if (req != null && req.Count > 0)
            {
                foreach (var p in req)
                {
                    if (postData.Length > 0)
                    {
                        postData.Append("&");
                    }
                    postData.Append(p.Key);
                    postData.Append("=");
                    postData.Append(p.Value);
                }
            }
            byte[] byteData = Encoding.GetEncoding("UTF-8").GetBytes(postData.ToString());
            try
            {

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.ContentType = "application/x-www-from-urlencoded";
                request.Referer = url;
                request.Accept = "*/*";
                request.Timeout = 30 * 1000;
                request.UserAgent = "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.1; SV1; .NET CLR 2.0.50727; .NET CLR 3.0.04506.648; .NET CLR 3.0.4506.2152; .NET CLR 3.5.30729)";
                request.Method = "POST";
                request.ContentLength = byteData.Length;

                Stream stream = request.GetRequestStream();
                stream.Write(byteData, 0, byteData.Length);
                stream.Flush();
                stream.Close();
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream backStream = response.GetResponseStream();
                StreamReader sr = new StreamReader(backStream, Encoding.GetEncoding("UTF-8"));
                result = sr.ReadToEnd();
                sr.Close();
                backStream.Close();
                response.Close();
                request.Abort();
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return result;
        }
    }
}
