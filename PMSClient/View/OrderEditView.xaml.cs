using PMSClient.BasicService;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using System.Text.RegularExpressions;

namespace PMSClient.View
{
    /// <summary>
    /// OrderEditView.xaml 的交互逻辑
    /// </summary>
    public partial class OrderEditView : UserControl
    {
        public OrderEditView()
        {
            InitializeComponent();
        }

        private void btnTipCompositionStd_Click(object sender, RoutedEventArgs e)
        {
            PMSMessageBox msg = new PMSClient.PMSMessageBox();
            try
            {
                msg.MessageTitle = "规范的成分写法";
                var filepath = System.IO.Path.Combine(PMSFolderPath.Documents, "stdcomposition.txt");
                var content = File.ReadAllText(filepath);
                msg.MessageContent = content;
                msg.ShowDialog();
            }
            catch (Exception ex)
            {
                PMSHelper.CurrentLog.Error(ex);
            }


        }

        private void btnAutoTransferComposition_Click(object sender, RoutedEventArgs e)
        {

            var source = txtCompositionOrignal.Text.Trim();
            if (string.IsNullOrEmpty(source))
            {
                return;
            }
            //成分标准化
            string std = source.Replace(" ", "")
                                               .Replace("(", "")
                                               .Replace(")", "")
                                               .Replace("atomic", "")
                                               .Replace("%", "");
            //成分缩写
            string abbr = "";
            if (IsCIGS(std))
            {
                abbr = "CIGS";
            }
            else
            {
                var matches = System.Text.RegularExpressions.Regex.Matches(std, @"[a-zA-Z]", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                foreach (var item in matches)
                {
                    abbr += item.ToString();
                }
            }
            PMSMethods.SetTextBox(txtCompositionStandard, std);
            PMSMethods.SetTextBox(txtCompositionAbbr, abbr);

        }

        private bool IsCIGS(string source)
        {
            var s = source.ToLower();
            return s.Contains("cu") && s.Contains("in") && s.Contains("ga") && s.Contains("se");
        }

        private void btnToOne_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                PMSClient.ToolWindow.CompositionToOne cto = new ToolWindow.CompositionToOne();
                cto.Show();
            }
            catch (Exception ex)
            {
                PMSHelper.CurrentLog.Error(ex);
            }
        }

        private void BtnSendCustomer_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            if (button.Name == "BtnSendTCB")
            {
                PMSMethods.SetTextBox(TxtShipTo, "TCB");

            }
            else
            {
                PMSMethods.SetTextBox(TxtShipTo, cboCustomerNames.SelectedItem.ToString());

            }

            e.Handled = true;
        }

        private void BtnGenerateDrawingNumber_Click(object sender, RoutedEventArgs e)
        {
            string s = TxtDimension.Text.Trim();
            if (string.IsNullOrEmpty(s))
                return;
            string pattern = @"\d+\.\d+|\d+";
            var matches = Regex.Matches(s, pattern);
            string d = matches[0].Value;
            string t = matches[1].Value;
            string drawing = $"TC-{d}-{t}";
            PMSMethods.SetTextBox(TxtDrawing, drawing);

        }

        private void BtnBasicRequirement1_Click(object sender, RoutedEventArgs e)
        {
            string s = @"TD±0.1 TH±0.1 Ra<1.6 FR=2";
            PMSMethods.SetTextBox(TxtDimensionDetails, s);

        }

        private void BtnBasicRequirement2_Click(object sender, RoutedEventArgs e)
        {
            string s = @"TD-0+0.1 TH-0+0.1 Ra<1.6 FR=2";
            PMSMethods.SetTextBox(TxtDimensionDetails, s);

        }

        private void BtnBasicRequirement3_Click(object sender, RoutedEventArgs e)
        {
            string s = @"TD-0.1+0 TH-0.1+0 Ra<1.6 FR=2";
            PMSMethods.SetTextBox(TxtDimensionDetails, s);

        }
        private void BtnBasicRequirement4_Click(object sender, RoutedEventArgs e)
        {
            PMSMethods.SetTextBoxAppend(TxtDimensionDetails, " PL=0.1 FT=0.1");

        }
        private void BtnAcceptDefects_Click(object sender, RoutedEventArgs e)
        {
            string s = @"ρ>3g/cm3 允许小缺陷";
            PMSMethods.SetTextBox(TxtAcceptDefects, s);
        }

        private void BtnPurity1_Click(object sender, RoutedEventArgs e)
        {
            PMSMethods.SetTextBox(TxtPurity, "99.990%");
        }

        private void BtnPurity2_Click(object sender, RoutedEventArgs e)
        {
            PMSMethods.SetTextBox(TxtPurity, "99.995%");

        }

        private void BtnPurity3_Click(object sender, RoutedEventArgs e)
        {
            PMSMethods.SetTextBox(TxtPurity, "99.999%");

        }

        private void BtnCurve_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            if (btn != null)
            {
                PMSMethods.SetTextBoxAppend(TxtLaserNeed, $"{btn.Content};");
            }
        }

        private void BtnUseMold_Click(object sender, RoutedEventArgs e)
        {
            PMSMethods.SetTextBoxAppend(TxtSpecialRequirement, "使用模具[];");

        }

        private void BtnBonding_Click(object sender, RoutedEventArgs e)
        {
            PMSMethods.SetTextBoxAppend(TxtSpecialRequirement, "成都绑定;");

        }

        private void BtnSecondDimension_Click(object sender, RoutedEventArgs e)
        {
            PMSMethods.SetTextBox(TxtSecondDimension, TxtDimension.Text);
        }

        private void BtnSampleNeed_Click(object sender, RoutedEventArgs e)
        {
            PMSMethods.SetTextBox(TxtSampleNeed, "块15gx1 粉15gx1");
        }

        private void BtnNoSample1_Click(object sender, RoutedEventArgs e)
        {
            PMSMethods.SetTextBox(TxtSampleNeed, "无需样品");
        }
        private void BtnNoSample2_Click(object sender, RoutedEventArgs e)
        {
            PMSMethods.SetTextBox(TxtSampleForAnlysis, "无需样品");
        }
        private void BtnSampleForAnlysis_Click(object sender, RoutedEventArgs e)
        {
            PMSMethods.SetTextBox(TxtSampleForAnlysis, "块15gx1 粉15gx1");
        }

        private void BtnCurveAuto_Click(object sender, RoutedEventArgs e)
        {
            string customername = cboCustomerNames.SelectedItem.ToString();
            if (customername.Contains("Bridgeline"))
            {
                PMSMethods.SetTextBoxAppend(TxtLaserNeed, $"靶+单面+边缘+ID;背板+单面+边缘+成分缩写ID;");
            }

        }

        private void BtnLaserEditor_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (TxtLaserNeed == null) return;
                var dialog = new Components.LaserNeed.LaserNeedResult();
                dialog.KeyStrings = TxtLaserNeed.Text.Trim();
                dialog.ShowDialog();
                if (dialog.DialogResult == true)
                {
                    PMSMethods.SetTextBox(TxtLaserNeed, dialog.KeyStrings);
                }
            }
            catch (Exception)
            {

            }
        }
    }
}
