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
using PMSCommon;

namespace PMSClient.View
{
    /// <summary>
    /// MaterialNeedEditView.xaml 的交互逻辑
    /// </summary>
    public partial class MaterialNeedEditView : UserControl
    {
        public MaterialNeedEditView()
        {
            InitializeComponent();
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

        private void BtnDefaultMemo_Click(object sender, RoutedEventArgs e)
        {
            PMSMethods.SetTextBox(TxtRemark, "第1次补料");

        }

        private void BtnReOrderReason_Click(object sender, RoutedEventArgs e)
        {
            var tool = new ToolWindow.ReOrderReason();
            tool.ShowDialog();
            if (!string.IsNullOrEmpty(tool.WindowContent))
            {
                PMSMethods.SetTextBoxAppend(TxtRemark, tool.WindowContent);
            }
        }
    }
}
