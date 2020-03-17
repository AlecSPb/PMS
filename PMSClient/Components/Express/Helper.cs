using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace PMSClient.Express
{
    public static class Helper
    {

        public static string Encrypt(string str,string keyvalue,string charset = "utf-8")
        {
            if (keyvalue != null)
            {
                return ToBase64(ToMD5(str + keyvalue, charset), charset);
            }
            return ToBase64(ToMD5(str, charset), charset);
        }

        /// <summary>
        /// 转换为base64
        /// </summary>
        /// <param name="str"></param>
        /// <param name="charset"></param>
        /// <returns></returns>
        public static string ToBase64(string str, string charset = "utf-8")
        {
            return Convert.ToBase64String(Encoding.GetEncoding(charset).GetBytes(str));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="str"></param>
        /// <param name="charset"></param>
        /// <returns></returns>
        public static string ToMD5(string str, string charset = "utf-8")
        {
            byte[] buffer = Encoding.GetEncoding(charset).GetBytes(str);
            try
            {
                MD5CryptoServiceProvider provider = new MD5CryptoServiceProvider();
                byte[] hash = provider.ComputeHash(buffer);
                string result = "";
                foreach (var item in hash)
                {
                    if (item < 16)
                    {
                        result += "0" + item.ToString("X");
                    }
                    else
                    {
                        result += item.ToString("X");
                    }
                }
                return result.ToLower();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
