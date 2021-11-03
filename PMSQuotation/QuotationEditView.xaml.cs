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
using PMSQuotation.Services;

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

        private void BtnEditCustomerInfo_Click(object sender, RoutedEventArgs e)
        {
            var win = new Tools.ContactEdit();
            win.SetContactWithStrValue(TxtContactInfo_Customer.Text.Trim());
            if (win.ShowDialog() == true)
            {
                PMSQuotation.Helpers.PMSMethods.SetTextBox(TxtContactInfo_Customer, win.GetContactInStrValue());
            }
        }

        private void BtnEditSelfInfo_Click(object sender, RoutedEventArgs e)
        {
            var win = new Tools.ContactEdit();
            win.SetContactWithStrValue(TxtContactInfo_Self.Text.Trim());
            if (win.ShowDialog() == true)
            {
                PMSQuotation.Helpers.PMSMethods.SetTextBox(TxtContactInfo_Self, win.GetContactInStrValue());
            }
        }

        private void BtnLot_Click(object sender, RoutedEventArgs e)
        {
            PMSQuotation.Helpers.PMSMethods.SetTextBox(TxtLot, Helpers.QuotationHelper.GetDefaultLot());
        }

        private void Btn1Mon_Click(object sender, RoutedEventArgs e)
        {
            DptExpirationTime.SelectedDate = DateTime.Now.AddMonths(1);
        }
        private void Btn2Mon_Click(object sender, RoutedEventArgs e)
        {
            DptExpirationTime.SelectedDate = DateTime.Now.AddMonths(2);
        }
        private void Btn3Mon_Click(object sender, RoutedEventArgs e)
        {
            DptExpirationTime.SelectedDate = DateTime.Now.AddMonths(3);
        }

        private void BtnVAT_Click(object sender, RoutedEventArgs e)
        {
            var model = this.DataContext as QuotationEditVM;
            if (model.CurrentQuotation != null)
            {
                var result = new CalculationService().GetTotalCostWithOutTaxFee(model.CurrentQuotation);
                double vat_rate = 0;
                double.TryParse(new QuotationDbService().GetDataDictByKey("vat_rate").DataValue, out vat_rate);

                double tax_fee = result.Item1 * vat_rate;
                string tax_remark = $"Current VAT Tax Rate={vat_rate * 100}%";
                XSHelper.XS.MessageBox.ShowInfo($"VAT rate is {vat_rate}");
                Helpers.PMSMethods.SetTextBox(TxtTaxFee, tax_fee.ToString());
                Helpers.PMSMethods.SetTextBox(TxtTaxRemark, tax_remark);
            }
        }

        private void BtnNone_Click(object sender, RoutedEventArgs e)
        {
            Helpers.PMSMethods.SetTextBox(TxtTaxFee, "0");
            Helpers.PMSMethods.SetTextBox(TxtTaxRemark, "No Tax");
        }
    }
}
