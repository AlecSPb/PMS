using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using PMSClient.MainService;
using System.Text.RegularExpressions;
using System.IO;


namespace PMSClient.CheckLogic
{
    /// <summary>
    /// RecordTestCheckDialog.xaml 的交互逻辑
    /// </summary>
    public partial class RecordTestCheckDialog : Window
    {
        public RecordTestCheckDialog()
        {
            InitializeComponent();
        }

        private void BtnCheck_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder sb = new StringBuilder();

            using (var service = new RecordTestServiceClient())
            {
                var models = service.GetUnCheckedRecordTest();
                foreach (var item in models)
                {
                    sb.Append(Check(item));
                }
            }

            TxtResult.Text = "检查结果如下(仅显示异常结果)" + Environment.NewLine + sb.ToString();
        }

        /// <summary>
        /// 检查各个项目
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public string Check(DcRecordTest model)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(model.ProductID);
            sb.Append(" ");
            sb.AppendLine(model.Composition);

            //执行检查逻辑
            if (ChkDensity.IsChecked == true)
            {
                sb.Append(CheckDensity(model.Composition, double.Parse(model.Density)));
            }

            if (ChkDimension.IsChecked == true)
            {
                sb.Append(CheckDimension(model.DimensionActual, model.Dimension));
            }

            //if (ChkXRF.IsChecked == true)
            //{
            //    sb.AppendLine(CheckXRF(model.CompositionXRF));
            //}

            sb.AppendLine();
            return sb.ToString();
        }


        #region 具体检查逻辑
        /// <summary>
        /// 检查密度是否在正常范围之内
        /// </summary>
        /// <param name="fact"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public string CheckDensity(string abbr, double fact)
        {
            StringBuilder sb = new StringBuilder();

            string[] lines = File.ReadAllLines("densitychecklist.txt");

            Dictionary<string, Density> densities = new Dictionary<string, Density>();
            foreach (var line in lines)
            {
                string[] temp = line.Split(new string[] { "+" }, StringSplitOptions.RemoveEmptyEntries);
                var density = new Density
                {
                    Composition = temp[0],
                    Min = double.Parse(temp[1]),
                    Max = double.Parse(temp[2])
                };
                densities.Add(temp[0], density);
            }

            string key = "";
            #region 判定类型
            if (abbr.Contains("Cu")
                && abbr.Contains("In")
                && abbr.Contains("Ga")
                && abbr.Contains("Se"))
            {
                key = "CIGS";
            }
            if (abbr.Contains("Cu")
                && !abbr.Contains("In")
                && abbr.Contains("Ga")
                && abbr.Contains("Se"))
            {
                key = "CuGaSe";
            }
            else if (abbr.Contains("Se")
                && abbr.Contains("As")
                && abbr.Contains("Ge"))
            {
                key = "SAG";
            }
            else if (abbr.Contains("Se")
                && abbr.Contains("In")
                && !abbr.Contains("Ga") && !abbr.Contains("Cu"))
            {
                key = "InSe";
            }
            else if (abbr.Contains("S")
                && abbr.Contains("In")
                && !abbr.Contains("Ga") && !abbr.Contains("Cu") && !abbr.Contains("Se") && !abbr.Contains("As"))
            {
                key = "InS";
            }
            else if (abbr.Contains("Se")
                && abbr.Contains("Bi")
                && abbr.Contains("Te"))
            {
                key = "BiTeSe";
            }
            else if (abbr.Contains("Sb")
                && abbr.Contains("Bi")
                && abbr.Contains("Te"))
            {
                key = "BiSbTe";
            }
            #endregion

            double min = 0, max = 0;
            if (densities.ContainsKey(key))
            {
                var choice = densities[key];
                min = choice.Min;
                max = choice.Max;
                if (fact > min && fact < max)
                {
                }
                else
                {
                    sb.AppendLine($"[密度]{fact}超出范围{key}:{min}-{max}");
                }
            }
            else
            {
                sb.AppendLine("[密度]库里没有预设判定值，请人工判定");
            }



            return sb.ToString();
        }
        /// <summary>
        /// 对比检查要求尺寸和实际尺寸
        /// </summary>
        /// <param name="fact"></param>
        /// <param name="expect"></param>
        /// <returns></returns>
        public string CheckDimension(string fact, string expect)
        {
            StringBuilder sb = new StringBuilder();
            //string pattern = @"[1-9]\d*.\d*|0.\d*[1-9]\d*";
            //匹配整数或者小数
            string pattern = @"[0-9]+([.]{1}[0-9]+){0,1}";

            const double variable_low = 0.2d;
            const double variable_upper = 0.1d;
            const double variable_t = 0.1d;

            double fact_d = 0, fact_h = 0, expect_d = 0, expect_h = 0;


            try
            {
                var result_expect = Regex.Matches(expect, pattern);
                if (result_expect.Count >= 2)
                {
                    expect_d = double.Parse(result_expect[0].Value);
                    expect_h = double.Parse(result_expect[1].Value);
                }
                var result_fact = Regex.Matches(fact, pattern);
                if (result_fact.Count >= 2)
                {
                    fact_d = double.Parse(result_fact[0].Value);
                    fact_h = double.Parse(result_fact[1].Value);
                }
                if (fact_d > expect_d - variable_low && fact_d < expect_d + variable_upper)
                {
                    //sb.Append("直径正常;");
                }
                else
                {
                    sb.AppendLine($"[直径]异常,超出-{variable_low}+{variable_upper};");

                }

                if (fact_h > expect_h - variable_t && fact_h < expect_h + variable_t)
                {
                    //sb.AppendLine("厚度未发现异常;");
                }
                else
                {
                    sb.AppendLine($"[厚度]异常,超出±{variable_t};");

                }
            }
            catch (Exception)
            {
                sb.AppendLine("检查出错，核对尺寸格式");
            }

            return sb.ToString();
        }
        public string CheckXRF(string fact)
        {
            return "XRF未发现异常";
        }
        #endregion

        private void BtnDensityCheckList_Click(object sender, RoutedEventArgs e)
        {
            PMSDialogService.Show("缩写+密度最小值+密度最大值");
            System.Diagnostics.Process.Start("densitychecklist.txt");
        }
    }
}
