using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMSClient.ReportsHelperNew
{
    /// <summary>
    /// 用于报告当中一些复杂的数据处理
    /// </summary>
    public class ReportDataProcessHelper
    {

        /// <summary>
        /// 计算韩国的XRF成分的标准差,并追加标准差数据到XRF数据团后面
        /// </summary>
        /// <param name="xrf">xrf的csv格式的数据</param>
        /// <returns>追加了stddev到xrf字符串的尾行</returns>
        public string AppendStdDev(string xrf)
        {
            string[] lines = xrf.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            string[] columns_first_line = lines.First().Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);

            //建立一个二维数组用来存储数据
            double[,] data = new double[lines.Length - 2, columns_first_line.Length - 1];

            for (int i = 1; i < lines.Length - 1; i++)
            {
                string[] current_row_columns = lines[i].Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                for (int j = 1; j < current_row_columns.Length; j++)
                {
                    data[i - 1, j - 1] = double.Parse(current_row_columns[j]);
                }
            }

            StringBuilder sb = new StringBuilder();
            sb.Append("STDEV");
            //column，按列读取每个成分的数据
            for (int j = 0; j < data.GetLength(1); j++)
            {
                //row
                double[] numbers = new double[data.GetLength(0)];
                for (int i = 0; i < data.GetLength(0); i++)
                {
                    numbers[i] = data[i, j];
                }
                double std_dev = CalculateStdDev(numbers);
                sb.Append(",");
                sb.Append(std_dev.ToString("F2"));
            }

            //追加标准差行到最后一行
            return xrf + sb.ToString();
        }


        /// <summary>
        /// 获取一组数字的标准差
        /// </summary>
        /// <param name="numbers"></param>
        /// <returns></returns>
        private double CalculateStdDev(double[] numbers)
        {
            double ret = 0;
            if (numbers.Count() > 0)
            {
                double avg = numbers.Average();

                double sum = numbers.Sum(d => Math.Pow(d - avg, 2));

                ret = Math.Sqrt(sum / numbers.Count());
            }
            return ret;
        }


    }
}
