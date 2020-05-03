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
    public partial class ConsumablePurchaseEditView : UserControl
    {
        public ConsumablePurchaseEditView()
        {
            InitializeComponent();
        }

        private void BtnGood_Click(object sender, RoutedEventArgs e)
        {
            PMSMethods.SetTextBoxAppend(TxtRemark, $"{DateTime.Now.ToString("yyyy-MM-dd")}收到货物;");
        }

        private void BtnInvoice_Click(object sender, RoutedEventArgs e)
        {
            PMSMethods.SetTextBoxAppend(TxtRemark, $"{DateTime.Now.ToString("yyyy-MM-dd")}收到发票;");
        }

        private void BtnPay_Click(object sender, RoutedEventArgs e)
        {
            PMSMethods.SetTextBoxAppend(TxtRemark, $"{DateTime.Now.ToString("yyyy-MM-dd")}付款;");

        }
    }
}
