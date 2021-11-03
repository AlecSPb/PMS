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
using GalaSoft.MvvmLight.Messaging;

namespace PMSQuotation
{
    /// <summary>
    /// QuotationEditView.xaml 的交互逻辑
    /// </summary>
    public partial class QuotationEditView : Window
    {
        public QuotationEditView()
        {
            InitializeComponent();
            this.Height = 800;
            Messenger.Default.Register<NotificationMessage>(this, "MSG", ActionDo);
        }

        private void ActionDo(NotificationMessage obj)
        {
            if (obj.Notification == "CloseEditWindow")
            {
                this.Close();
            }
        }

        private void BtnCHooseCustomerInfo_Click(object sender, RoutedEventArgs e)
        {
            var win = new Tools.ContactEdit();
            win.SetContactWithStrValue(TxtContactInfo_Customer.Text.Trim());
            if (win.ShowDialog()==true)
            {
                PMSQuotation.Helpers.PMSMethods.SetTextBox(TxtContactInfo_Customer, win.GetContactInStrValue());
            }
        }

        private void BtnChooseSelfInfo_Click(object sender, RoutedEventArgs e)
        {
            var win = new Tools.ContactEdit();
            win.SetContactWithStrValue(TxtContactInfo_Self.Text.Trim());
            if (win.ShowDialog() == true)
            {
                PMSQuotation.Helpers.PMSMethods.SetTextBox(TxtContactInfo_Self, win.GetContactInStrValue());
            }
        }
    }
}
