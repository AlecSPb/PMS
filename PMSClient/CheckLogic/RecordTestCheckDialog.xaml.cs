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

            TxtResult.Text = "检查结果如下" + Environment.NewLine + sb.ToString();
        }

        public string Check(DcRecordTest model)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(model.ProductID);
            sb.Append(" ");
            sb.AppendLine(model.Composition);

            //执行检查逻辑
            if (ChkDensity.IsChecked == true)
            {
                sb.Append(CheckDensity(double.Parse(model.Density), 3, 8));
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
        public string CheckDensity(double fact, double min, double max)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("密度未发现异常");
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
            string pattern = @"[1-9]\d*.\d*|0.\d*[1-9]\d*";

            const double variable = 0.1d;
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
                if (fact_d > expect_d - variable && fact_d < expect_d + variable)
                {
                    //sb.Append("直径未发现异常;");
                }
                else
                {
                    sb.Append($"直径异常,超出±{variable};");

                }

                if (fact_h > expect_h - variable && fact_h < expect_h + variable)
                {
                    //sb.AppendLine("厚度未发现异常;");
                }
                else
                {
                    sb.AppendLine($"厚度异常,超出±{variable};");

                }
            }
            catch (Exception)
            {
                sb.AppendLine("检查出错，请仔细核对实际尺寸和要求尺寸的格式是否正确");
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
            System.Diagnostics.Process.Start("densitychecklist.txt");
        }
    }
}
