using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMSClient.Helpers
{
    public class XRFResult
    {
        public XRFResult()
        {
            Average = new List<double>();
            Max = new List<double>();
            Min = new List<double>();
            Compostions = new List<List<double>>();
        }

        public List<double> Average { get; set; }
        public List<double> Max { get; set; }
        public List<double> Min { get; set; }

        public List<List<double>> Compostions { get; set; }
    }

    /// <summary>
    /// 主要用于230 瑞典或潮州的CIGSe
    /// </summary>
    public static class XRFCompositionAnalysis
    {

        public static XRFResult Anlysis(string xrf)
        {
            XRFResult result = new XRFResult();

            try
            {
                //判定是否包含成分
                if (!xrf.StartsWith("No."))
                {
                    return result;
                }

                string[] lines = xrf.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);

                //存储原始数据，不包含首行，首列,平均
                //List<List<double>> raw_data = new List<List<double>>();
                for (int i = 1; i < lines.Length - 1; i++)
                {
                    string line = lines[i];

                    if (string.IsNullOrEmpty(line) || line.StartsWith("No."))
                    {
                        continue;
                    }

                    string[] temps = line.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                    List<double> line_data = new List<double>();

                    for (int j = 1; j < temps.Length; j++)
                    {
                        double n = 0;
                        double.TryParse(temps[j], out n);
                        line_data.Add(n);
                    }
                    result.Compostions.Add(line_data);
                }

                //只有在xrf元素数据小于5的时候进行写入
                if (result.Compostions.Count > 0 && result.Compostions[0].Count <= 4)
                {
                    //最大,最小,平均
                    for (int col = 0; col < result.Compostions[0].Count; col++)
                    {
                        List<double> temp = new List<double>();
                        temp.Clear();
                        for (int row = 0; row < result.Compostions.Count; row++)
                        {
                            temp.Add(result.Compostions[row][col]);
                        }
                        result.Max.Add(temp.Max());
                        result.Min.Add(temp.Min());
                        result.Average.Add(temp.Average());
                    }
                }

            }
            catch (Exception ex)
            {

             PMSHelper.CurrentLog.Error(ex);
            }

            return result;
        }


    }
}
