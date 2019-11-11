using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using PMSClient.ToolWindow.Model;

namespace PMSClient.ToolWindow
{
    public class CompositionSimulatorHelper
    {
        private int recordCount;
        private List<SingleElement> elements;


        public CompositionSimulatorHelper()
        {
            elements = new List<SingleElement>();
            recordCount = 5;
        }

        private void ReadDataFromText(string input)
        {
            try
            {
                string[] lines = input.Split(Environment.NewLine.ToCharArray(),
                    StringSplitOptions.RemoveEmptyEntries);

                //读取记录数目
                int.TryParse(lines[0], out recordCount);
                //读取元素种类
                for (int i = 1; i < lines.Length; i++)
                {
                    string[] arrays = lines[i].Split('+');
                    SingleElement s = new SingleElement();
                    s.Element = arrays[0];
                    s.Ratio = double.Parse(arrays[1]);
                    //判断文件默认有没有设定Offset
                    if (arrays.Length == 3)
                    {
                        s.Offset = double.Parse(arrays[2]);
                    }
                    elements.Add(s);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        /// <summary>
        /// get csv format content
        /// </summary>
        /// <param name="input">
        /// 5
        /// Cu+22.8
        /// In+22.8
        /// </param>
        /// <returns></returns>
        public string SimulateCompositionToCsvFormat(string input)
        {
            ReadDataFromText(input);

            ProcessData();

            return FormatOutput();
        }

        private string FormatOutput()
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Clear();
                //write the title
                sb.Append("No.");
                elements.ForEach(s =>
                {
                    sb.Append(",");
                    sb.Append(s.Element + " atm%");
                });
                sb.AppendLine();

                //write the content
                for (int i = 0; i < recordCount; i++)
                {
                    sb.Append(i + 1);
                    elements.ForEach(s =>
                    {
                        sb.Append(",");
                        sb.Append((s.RealValues[i]).ToString("F2"));
                    });
                    sb.AppendLine();
                }

                //write the average
                sb.Append("Average");
                elements.ForEach(s =>
                {
                    sb.Append(",");
                    sb.Append((s.Average).ToString("F2"));
                });
                sb.AppendLine();

                return sb.ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void ProcessData()
        {
            //Process Data
            try
            {
                double AllAt = elements.Sum(s => s.Ratio);


                //convert ratio to 100%
                elements.ForEach(s =>
                {
                    s.Ratio = s.Ratio / AllAt;
                });

                Random r = new Random();

                for (int i = 0; i < recordCount; i++)
                {
                    double sum = 0;
                    for (int j = 0; j < elements.Count; j++)
                    {
                        var s = elements[j];
                        double temp = 0;
                        //last element   for special way
                        if (j < elements.Count - 1)
                        {
                            //核心随机算法
                            temp = s.Ratio * 100 + r.NextDouble() * s.Offset * 2 - s.Offset;
                            sum += temp;
                        }
                        else
                        {
                            temp = 100 - sum;
                        }
                        s.RealValues.Add(temp);
                    }
                }

                elements.ForEach(s =>
                {
                    s.Average = s.RealValues.Average();
                });
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
