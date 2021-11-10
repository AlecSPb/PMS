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
using PMSQuotation.Models;
using PMSQuotation.Services;
using PMSQuotation.Tools;

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
            DptExpirationTime.SelectedDate = DateTime.Now.AddMonths(3);
        }
        private void Btn2Mon_Click(object sender, RoutedEventArgs e)
        {
            DptExpirationTime.SelectedDate = DateTime.Now.AddMonths(6);
        }
        private void Btn3Mon_Click(object sender, RoutedEventArgs e)
        {
            DptExpirationTime.SelectedDate = DateTime.Now.AddMonths(9);
        }

        private void BtnNone_Click(object sender, RoutedEventArgs e)
        {
            Helpers.PMSMethods.SetTextBox(TxtTaxFee, "0");
            Helpers.PMSMethods.SetTextBox(TxtTaxRemark, "No Tax");
        }

        private void BtnToolPackageFee_Click(object sender, RoutedEventArgs e)
        {
            var win1 = new ToolExtra("Tool Package Fee");
            win1.SetJson(TxtPackageRemark.Text, "package_fee");
            if (win1.ShowDialog() == true)
            {
                Helpers.PMSMethods.SetTextBox(TxtPackageFee, SumExtraFee(win1.Items.ToList()).ToString("F2"));
                Helpers.PMSMethods.SetTextBox(TxtPackageRemark, win1.GetJson());
            }
        }

        private void BtnToolCustomFee_Click(object sender, RoutedEventArgs e)
        {
            var win1 = new ToolExtra("Tool Custom Fee");
            win1.SetJson(TxtCustomRemark.Text, "custom_fee");
            if (win1.ShowDialog() == true)
            {
                Helpers.PMSMethods.SetTextBox(TxtCustomFee, SumExtraFee(win1.Items.ToList()).ToString("F2"));
                Helpers.PMSMethods.SetTextBox(TxtCustomRemark, win1.GetJson());
            }
        }

        private void BtnToolShippingFee_Click(object sender, RoutedEventArgs e)
        {
            var win1 = new ToolExtra("Tool Shipping Fee");
            win1.SetJson(TxtShippingRemark.Text, "shipping_fee");
            if (win1.ShowDialog() == true)
            {
                Helpers.PMSMethods.SetTextBox(TxtShippingFee, SumExtraFee(win1.Items.ToList()).ToString("F2"));
                Helpers.PMSMethods.SetTextBox(TxtShippingRemark, win1.GetJson());
            }
        }

        private double SumExtraFee(List<CostItemExtra> items)
        {
            double sum = 0;
            foreach (var item in items)
            {
                sum += item.UnitPrice * item.Quantity;
            }
            return sum;
        }

        private void BtnToolCustomFeeReset_Click(object sender, RoutedEventArgs e)
        {
            Helpers.PMSMethods.SetTextBox(TxtCustomFee, "0");
            Helpers.PMSMethods.SetTextBox(TxtCustomRemark, "");
        }

        private void BtnToolShippingFeeReset_Click(object sender, RoutedEventArgs e)
        {
            Helpers.PMSMethods.SetTextBox(TxtShippingFee, "0");
            Helpers.PMSMethods.SetTextBox(TxtShippingRemark,"");
        }

        private void BtnToolPackageFeeReset_Click(object sender, RoutedEventArgs e)
        {
            Helpers.PMSMethods.SetTextBox(TxtPackageFee, "0");
            Helpers.PMSMethods.SetTextBox(TxtPackageRemark, "");
        }
    }
}
