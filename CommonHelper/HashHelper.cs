using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace CommonHelper
{
    /// <summary>
    /// HashHelper
    /// </summary>
    public static class HashHelper
    {
        /// <summary>
        /// 获取bytes形式的md5字符串
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static byte[] GetMD5(byte[] input)
        {
            MD5 hash = MD5.Create();
            return hash.ComputeHash(input);
        }
        /// <summary>
        /// 获取md5的字符串 32位表示
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string GetMD5String(string input)
        {
            byte[] bytes = Encoding.Default.GetBytes(input);
            byte[] results = GetMD5(bytes);
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < results.Length; i++)
            {
                sb.Append(results[i].ToString("x2"));
            }
            return sb.ToString();
        }
    }
}
