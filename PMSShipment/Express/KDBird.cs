using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json;

namespace PMSShipment.Express
{
    public class KDBird
    {
        /// <summary>
        /// json方式查询订单实时追踪
        /// </summary>
        /// <returns></returns>
        public string GetOrderTracesByJson(Request request)
        {
            string requestData =JsonConvert.SerializeObject(request);

            Dictionary<string, string> param = new Dictionary<string, string>();
            param.Add("RequestData", HttpUtility.UrlEncode(requestData, Encoding.UTF8));
            param.Add("EBusinessID", ID.EBusinessID);
            param.Add("RequestType", "1002");
            string dataSign = Helper.Encrypt(requestData, ID.ApiKey, "UTF-8");
            param.Add("DataSign", HttpUtility.UrlEncode(dataSign, Encoding.UTF8));
            param.Add("DataType", "2");

            string result = SendPost(Url.FormalUrl,param);

            //根据公司业务处理返回的信息......

            return result;
        }

        private string SendPost(string url,Dictionary<string,string> param)
        {
            string result = "";
            StringBuilder postData = new StringBuilder();
            if (param != null && param.Count > 0)
            {
                foreach (var p in param)
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
                request.ContentType = "application/x-www-form-urlencoded";
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
