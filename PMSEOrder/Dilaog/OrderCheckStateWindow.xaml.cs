using PMSEOrder.Model;
using PMSEOrder.Service;
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
using XSHelper;
using GalaSoft.MvvmLight.Messaging;

namespace PMSEOrder.Dilaog
{
    /// <summary>
    /// OrderCheckStateWindow.xaml 的交互逻辑
    /// </summary>
    public partial class OrderCheckStateWindow : Window
    {
        public OrderCheckStateWindow()
        {
            InitializeComponent();
        }

        private List<OrderCheckState> currentModels;
        public void SetDataSource(List<OrderCheckState> models)
        {
            if (models != null)
            {
                dg.ItemsSource = currentModels = models;
            }
        }

        private void BtnUpdateOrderState_Click(object sender, RoutedEventArgs e)
        {
            if (XS.MessageBox.ShowYesNo("Update order state to [Sent] which is true in the [exist in pms]?"))
            {
                var ds = new DataService();
                foreach (var item in currentModels)
                {
                    //System.Diagnostics.Debug.WriteLine($"{item.CurrentOrder.GUIDID}-{item.CurrentOrder.CustomerName}-{item.CheckState}");
                    if (item.ExistInPMS)
                    {
                        var order = item.CurrentOrder;
                        order.OrderState = OrderState.Sent.ToString();
                        ds.UpdateOrder(order);

                        Messenger.Default.Send(new NotificationMessage("RefreshMainWindow"), "MSG");
                    }
                }
            }
        }
    }
}
