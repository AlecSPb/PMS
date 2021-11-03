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
    /// QuotationItemEditView.xaml 的交互逻辑
    /// </summary>
    public partial class QuotationItemEditView : Window
    {
        public QuotationItemEditView()
        {
            InitializeComponent();
            Messenger.Default.Register<NotificationMessage>(this, "MSG", ActionDo);
        }

        private void ActionDo(NotificationMessage obj)
        {
            if (obj.Notification == "CloseItemEditWindow")
            {
                this.Close();
            }
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            var win = new Tools.SpecificationEdit();
            win.SetContactWithStrValue(TxtSpecification.Text.Trim());
            if (win.ShowDialog() == true)
            {
                PMSQuotation.Helpers.PMSMethods.SetTextBox(TxtSpecification, win.GetContactInStrValue());
            }
        }
    }
}
