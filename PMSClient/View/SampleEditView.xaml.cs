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
using PMSClient.ToolWindow;
using PMSCommon;

namespace PMSClient.View
{
    /// <summary>
    /// FailureEditView.xaml 的交互逻辑
    /// </summary>
    public partial class SampleEditView : UserControl
    {
        public SampleEditView()
        {
            InitializeComponent();
        }

        private void BtnPrepared_Click(object sender, RoutedEventArgs e)
        {
            CboSampleTypes.SelectedItem = PMSCommon.SampleType.未核验.ToString();
            PMSMethods.SetTextBoxAppend(TxtProcess, $"{DateTime.Now.ToString("yyyy-MM-dd")}已准备;");
        }

        private void BtnChecked_Click(object sender, RoutedEventArgs e)
        {
            CboSampleTypes.SelectedItem = PMSCommon.SampleType.已核验.ToString();
            PMSMethods.SetTextBoxAppend(TxtProcess, $"{DateTime.Now.ToString("yyyy-MM-dd")}已核验;");
        }

        private void BtnSent_Click(object sender, RoutedEventArgs e)
        {
            CboSampleTypes.SelectedItem = PMSCommon.SampleType.已发出.ToString();
            PMSMethods.SetTextBoxAppend(TxtProcess, $"{DateTime.Now.ToString("yyyy-MM-dd")}已发出;");
        }

        private void BtnMoreTestResult_Click(object sender, RoutedEventArgs e)
        {
            SetKeyValue(TxtMoreTestResult);

        }

        private void BtnTestResult_Click(object sender, RoutedEventArgs e)
        {
            SetKeyValue(TxtTestResult);
        }
        private void SetKeyValue(TextBox textBox)
        {
            if (textBox == null) return;
            var dialog = new WPFControls.KeyValueTestResult();
            dialog.KeyStrings = textBox.Text.Trim();
            dialog.ShowDialog();
            if (dialog.DialogResult == true)
            {
                PMSMethods.SetTextBox(textBox, dialog.KeyStrings);
            }
        }
    }
}
