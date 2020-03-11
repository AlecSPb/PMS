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

namespace PMSClient.View
{
    /// <summary>
    /// OutsourceEditView.xaml 的交互逻辑
    /// </summary>
    public partial class OutsideProcessEditView : UserControl
    {
        public OutsideProcessEditView()
        {
            InitializeComponent();
        }

        private void BtnSend_Click(object sender, RoutedEventArgs e)
        {
            PMSMethods.SetTextBoxAppend(TxtRemark, $"{DateTime.Now.ToString("yyyy-MM-dd")}{BtnSend.Content};");
            CboState.SelectedItem = PMSCommon.OutsideProcessState.已发出.ToString();
        }

        private void BtnReceive_Click(object sender, RoutedEventArgs e)
        {
            PMSMethods.SetTextBoxAppend(TxtRemark, $"{DateTime.Now.ToString("yyyy-MM-dd")}{BtnReceive.Content};");
            CboState.SelectedItem = PMSCommon.OutsideProcessState.已取回.ToString();
        }

        private void BtnFail_Click(object sender, RoutedEventArgs e)
        {
            PMSMethods.SetTextBoxAppend(TxtRemark, $"{DateTime.Now.ToString("yyyy-MM-dd")}{BtnFail.Content};");
        }

        private void BtnAgain_Click(object sender, RoutedEventArgs e)
        {
            PMSMethods.SetTextBoxAppend(TxtRemark, $"{DateTime.Now.ToString("yyyy-MM-dd")}{BtnAgain.Content};");

        }
    }
}
