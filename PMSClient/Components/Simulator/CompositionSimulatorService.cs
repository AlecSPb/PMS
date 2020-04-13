using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMSClient.Simulator
{
    public class CompositionSimulatorService
    {
        private double defaultOffset = 0.2;
        public CompositionSimulatorService(double offset = 0.5)
        {
            defaultOffset = offset;
        }

        public string Simulate(string input_str)
        {
            string s = "";
            try
            {
                InputModel input = GetInputModel(input_str);
                OutputModel output = GetOutputModel(input);
                s = GetCsvResult(output);
            }
            catch (Exception)
            {

            }
            return s;
        }

        /// <summary>
        /// 读取输入字符串到输入模型
        /// </summary>
        /// <returns></returns>
        private InputModel GetInputModel(string input_str)
        {
            InputModel input = new InputModel();
            try
            {
                string[] lines = input_str.Split(Environment.NewLine.ToCharArray(),
                    StringSplitOptions.RemoveEmptyEntries);

                int record_count = 0;
                //读取记录数目
                int.TryParse(lines[0], out record_count);
                input.RecordCount = record_count;

                //读取元素种类
                for (int i = 1; i < lines.Length; i++)
                {
                    string[] arrays = lines[i].Split('+');
                    InputValue s = new InputValue();
                    s.Element = arrays[0];
                    s.Ratio = double.Parse(arrays[1]);
                    //判断文件默认有没有设定Offset
                    if (arrays.Length == 3)
                    {
                        s.Offset = double.Parse(arrays[2]);
                    }
                    else
                    {
                        s.Offset = defaultOffset;
                    }
                    input.Elements.Add(s);
                }
                //转换为标准数值
                double AllAt = input.Elements.Sum(s => s.Ratio);


                //convert ratio to 100%
                input.Elements.ForEach(s =>
                {
                    s.Ratio = s.Ratio / AllAt;
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return input;
        }

        /// <summary>
        /// 产生输出
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private OutputModel GetOutputModel(InputModel input)
        {
            OutputModel output = new OutputModel();
            StringBuilder title = new StringBuilder();
            title.Append("No.,");
            //循环生成标题
            for (int i = 0; i < input.Elements.Count(); i++)
            {
                title.Append(input.Elements[i].Element);
                title.Append(" atm%");
                if (i < input.Elements.Count() - 1)
                {
                    title.Append(",");
                }
            }

            output.FirstRow = title.ToString();

            Random r = new Random();
            //生成每一行的模拟值
            for (int i = 0; i < input.RecordCount; i++)
            {
                OutputValue output_row_value = new OutputValue();

                do
                {
                    output_row_value.Values.Clear();

                    output_row_value.RowNo = (i + 1).ToString();
                    double sum = 0;
                    //循环生成每一组的数值
                    for (int j = 0; j < input.Elements.Count; j++)
                    {
                        var s = input.Elements[j];
                        double temp = 0;
                        //last element   for special way
                        if (j < input.Elements.Count - 1)
                        {
                            //核心随机算法
                            temp = s.Ratio * 100 + r.NextDouble() * s.Offset * 2 - s.Offset;
                            //确定2位精度
                            temp = Math.Round(temp, 2, MidpointRounding.AwayFromZero);
                            sum += temp;
                        }
                        else
                        {
                            temp = 100 - sum;
                        }

                        output_row_value.Values.Add(Math.Round(temp, 2, MidpointRounding.AwayFromZero));
                    }
                } while (HasNegativeValues(output_row_value));//检查这组数字是否有负数,如果有，就再次生成一组

                output.RowValues.Add(output_row_value);
            }

            //生成平均值
            output.Average.RowNo = "Average";
            double sum_avg_row = 0;
            for (int i = 0; i < input.Elements.Count; i++)
            {
                double sum_col = 0;
                double avg_temp = 0;
                if (i < input.Elements.Count - 1)
                {
                    for (int j = 0; j < output.RowValues.Count; j++)
                    {
                        sum_col += output.RowValues[j].Values[i];
                    }
                    avg_temp = sum_col / output.RowValues.Count;
                    avg_temp = Math.Round(avg_temp, 2, MidpointRounding.AwayFromZero);
                    sum_avg_row += avg_temp;
                }
                else
                {
                    avg_temp = 100 - sum_avg_row;
                }
                output.Average.Values.Add(Math.Round(avg_temp, 2, MidpointRounding.AwayFromZero));
            }


            return output;
        }

        /// <summary>
        /// 检查生成的行数据里是否有负数
        /// </summary>
        /// <param name="currentRow"></param>
        /// <returns></returns>
        private bool HasNegativeValues(OutputValue currentRow)
        {
            if (currentRow == null) return true;
            bool hasNegativeValue = false;
            for (int i = 0; i < currentRow.Values.Count; i++)
            {
                if (currentRow.Values[i] <= 0)
                {
                    hasNegativeValue = true;
                    break;
                }
            }
            if (hasNegativeValue)
            {
                System.Diagnostics.Debug.WriteLine($"负值[{currentRow.RowNo}]{currentRow}");
            }
            return hasNegativeValue;
        }


        /// <summary>
        /// 转换输出模型到csv
        /// </summary>
        /// <param name="output"></param>
        /// <returns></returns>
        private string GetCsvResult(OutputModel output)
        {
            StringBuilder sb = new StringBuilder();
            //标题行
            sb.AppendLine(output.FirstRow);
            //数据行
            for (int i = 0; i < output.RowValues.Count; i++)
            {
                sb.Append(output.RowValues[i].RowNo);
                sb.Append(",");
                for (int j = 0; j < output.RowValues[i].Values.Count; j++)
                {
                    double at_temp = Math.Round(output.RowValues[i].Values[j], 2, MidpointRounding.AwayFromZero);
                    sb.Append(at_temp.ToString("F2"));
                    if (j < output.RowValues[i].Values.Count - 1)
                    {
                        sb.Append(",");
                    }
                }
                sb.AppendLine();
            }
            //平均行
            sb.Append(output.Average.RowNo);
            sb.Append(",");
            for (int i = 0; i < output.Average.Values.Count; i++)
            {
                double avg_temp = Math.Round(output.Average.Values[i], 2, MidpointRounding.AwayFromZero);
                sb.Append(avg_temp.ToString("F2"));
                if (i < output.Average.Values.Count - 1)
                {
                    sb.Append(",");
                }
            }
            sb.AppendLine();

            return sb.ToString();
        }

    }
}
