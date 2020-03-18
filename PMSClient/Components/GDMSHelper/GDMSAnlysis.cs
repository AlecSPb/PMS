using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMSClient.Components.GDMSHelper
{
    public class GDMSAnlysis
    {

        public string Run(string s)
        {
            var pairs = Anlysis(s);
            return GetGDMSString(pairs);
        }
        private List<GDMSPair> Anlysis(string s)
        {
            List<GDMSPair> pairs = new List<GDMSPair>();
            string[] lines = s.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);




            for (int i = 0; i < lines.Length; i++)
            {
                string[] words = lines[i].Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);

                //拆分小于2的不作处理
                if (words.Length >= 2)
                {
                    GDMSPair pair = new GDMSPair();
                    pair.Key = words[0];

                    //Value取剩余的连接结果
                    StringBuilder word_sb = new StringBuilder();
                    for (int j = 1; j < words.Length; j++)
                    {
                        word_sb.Append(words[j]);
                    }
                    pair.Value = word_sb.ToString();
                    pairs.Add(pair);
                }
            }


            return pairs;
        }

        private string GetGDMSString(List<GDMSPair> dict)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var item in dict)
            {
                sb.Append(item.Key);
                sb.Append("=");
                sb.Append(item.Value);
                sb.Append(";");
            }
            return RemoveNextLine(sb.ToString());
        }

        private string RemoveNextLine(string s)
        {
            return s.Replace("\r", "").Replace("\n", "");
        }
    }
}
