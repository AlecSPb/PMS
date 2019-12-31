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
using Microsoft.Win32;
using PMSClient.ToolWindow;

namespace PMSClient.View
{
    /// <summary>
    /// ProductEditView.xaml 的交互逻辑
    /// </summary>
    public partial class RecordTestEditView : UserControl
    {
        public RecordTestEditView()
        {
            InitializeComponent();
        }

        public string TargetWeight
        {
            get
            {
                return TxtWeight.Text;
            }
        }

        public string TargetDimension
        {
            get
            {
                return TxtDimension.Text;
            }
        }
        private void BtnCsv_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new OpenFileDialog();
            dialog.Filter = "CSV|*.csv";
            dialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);

            if (dialog.ShowDialog() == true)
            {
                string filename = dialog.FileName;
                if (File.Exists(filename))
                {
                    string result = File.ReadAllText(filename);
                    PMSMethods.SetTextBox(txtCompositionXRF, result.TrimEnd());
                }
            }
        }

        private void BtnResistance_Click(object sender, RoutedEventArgs e)
        {
            PMSMethods.SetTextBox(txtResistance, "Out Of Range");
        }

        private void BtnAddPlate_Click(object sender, RoutedEventArgs e)
        {
            PMSMethods.SetTextBoxAppend(txtRemark, "附有背板;");
        }


        private void BtnCalculator_Click(object sender, RoutedEventArgs e)
        {
            DensityCalculation calculator = new DensityCalculation();

            calculator.FillIn += (s, arg) =>
            {
                PMSMethods.SetTextBox(TxtDensity, arg);
            };
            calculator.Show();
        }

        private void btnSimulator_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            CompositionSimulator simulator = new CompositionSimulator();

            if (cboCustomerNames.Text.ToLower().Contains("bridgeline"))
            {
                simulator.SetForBridgeLine();
            }

            simulator.FillIn += (s, args) =>
            {
                PMSMethods.SetTextBox(txtCompositionXRF, args);
            };
            simulator.Show();
        }

        private void BtnBonding_Click(object sender, RoutedEventArgs e)
        {
            CboFollowUps.SelectedItem = "绑定";
        }

        private void BtnFail_Click(object sender, RoutedEventArgs e)
        {
            CboFollowUps.SelectedItem = "报废";
        }

        private void BtnDefects_Click(object sender, RoutedEventArgs e)
        {
            var win = new ToolWindow.TargetDefects();
            if (win.ShowDialog() == true)
            {
                PMSMethods.SetTextBoxAppend(TxtDefects, win.AllDefects);
            };

        }

        private void StackPanel_Click(object sender, RoutedEventArgs e)
        {
            if (TxtProductID.Text.Trim().Length < 7)
            {
                return;
            }

            Button btn = e.OriginalSource as Button;

            if (btn != null)
            {

                string postfix = btn.Content.ToString();
                if (postfix == "更多")
                {
                    var tool = new ToolWindow.ProductIDPostFix();
                    tool.ShowDialog();
                    if (tool.Postfix != string.Empty)
                    {
                        postfix = tool.Postfix;
                    }

                }

                if (postfix == "更多") return;

                string new_id = string.Empty;
                new_id = NewProductID(TxtProductID.Text.Trim(), postfix);
                PMSMethods.SetTextBox(TxtProductID, new_id);
            }

            e.Handled = true;
        }

        private string NewProductID(string old, string postfix)
        {
            string first = old.Substring(0, 7);
            return $"{first}{postfix}";
        }

        private void BtnRoughness_Click(object sender, RoutedEventArgs e)
        {
            PMSMethods.SetTextBox(TxtRoughness, "Ra-A=5um Ra-B=5um");

        }

        private void BtnWarpingNo_Click(object sender, RoutedEventArgs e)
        {
            PMSMethods.SetTextBox(TxtWarping, "无翘曲");
        }

        private void BtnWarpingYes_Click(object sender, RoutedEventArgs e)
        {
            PMSMethods.SetTextBox(TxtWarping, "有翘曲");
        }

        private void BtnQC_Click(object sender, RoutedEventArgs e)
        {
            PMSMethods.SetTextBox(TxtQC, "A=;B=;");
        }

        private void btnAddBP_Click(object sender, RoutedEventArgs e)
        {
            PMSMethods.SetTextBox(TxtBP, $"{DateTime.Today.ToString("yyMMdd")}-BP-1");
        }

        private void BtnDimensionMark_Click(object sender, RoutedEventArgs e)
        {
            string symbol = "★";
            string s = TxtDimension.Text.Replace("★", "");
            s = $"{symbol}{s}";
            PMSMethods.SetTextBox(TxtDimension, s);
        }

        private void BtnRecycle_Click(object sender, RoutedEventArgs e)
        {
            PMSMethods.SetTextBoxAppend(txtRemark, "回收重做;");
        }

        private void btnAddCSCAN_Click(object sender, RoutedEventArgs e)
        {
            PMSMethods.SetTextBox(TxtCSCAN, "明显孔洞");

        }

        private void BtnSpecial_Click(object sender, RoutedEventArgs e)
        {
            PMSMethods.SetTextBoxAppend(txtRemark, "异常发货;");

        }

        private void BtnTakeAway_Click(object sender, RoutedEventArgs e)
        {
            PMSMethods.SetTextBoxAppend(txtRemark, $"{DateTime.Now.ToString("yyMMdd")}[]拿走报废靶材;");
        }
    }
}
