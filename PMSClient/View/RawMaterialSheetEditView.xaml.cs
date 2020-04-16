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
    public partial class RawMaterialSheetEditView : UserControl
    {
        public RawMaterialSheetEditView()
        {
            InitializeComponent();
        }

        private void BtnGDMS1_Click(object sender, RoutedEventArgs e)
        {
            SetKeyValue(TxtGDMS);
        }

        private void BtnGDMS2_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new PMSClient.Components.GDMSHelper.GDMSHelper();
            if (dialog.ShowDialog() == true)
            {
                string s = dialog.GDMS;
                if (!string.IsNullOrEmpty(s))
                {
                    PMSMethods.SetTextBox(TxtGDMS, s);
                }
            }
        }

        private void BtnICPOES1_Click(object sender, RoutedEventArgs e)
        {
            SetKeyValue(TxtICPOES);
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
