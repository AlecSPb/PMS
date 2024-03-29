﻿using System;
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

        private void BtnUnitPriceTool_Click(object sender, RoutedEventArgs e)
        {
            var win = new Tools.UnitPriceCalculator();
            win.SetJson(TxtUnitPriceDetail.Text);
            if (win.ShowDialog() == true)
            {
                double sum = 0;
                //XSHelper.XS.MessageBox.ShowInfo(win.GetItemsJson());
                foreach (var item in win.Items)
                {
                    sum += item.ItemUnitPrice;
                }
                Helpers.PMSMethods.SetTextBox(TxtUnitPrice, sum.ToString());
                Helpers.PMSMethods.SetTextBox(TxtUnitPriceDetail, win.GetJson());

            }
        }

        private void BtnUnitEdit1_Click(object sender, RoutedEventArgs e)
        {
            Helpers.PMSMethods.SetTextBox(TxtUnit, "Piece");

        }

        private void BtnUnitEdit2_Click(object sender, RoutedEventArgs e)
        {
            Helpers.PMSMethods.SetTextBox(TxtUnit, "Set");

        }
    }
}
