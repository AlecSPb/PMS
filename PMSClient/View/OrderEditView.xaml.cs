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
                                               .Replace("(","")
                                               .Replace(")", "")
                                               .Replace("atomic", "")
                                               .Replace("%","");
            //成分缩写
            string abbr="";
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

        private void BtnBasicRequirement_Click(object sender, RoutedEventArgs e)
        {
            string s = @"TD±0.1mm TH±0.1mm Ra<50um FR=2mm";
            PMSMethods.SetTextBox(TxtDimensionDetails, s);

        }

        private void BtnAcceptDefects_Click(object sender, RoutedEventArgs e)
        {
            string s = @"ρ>3g/cm3 允许小缺陷";
            PMSMethods.SetTextBox(TxtAcceptDefects, s);
        }
    }
}
