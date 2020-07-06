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
using PMSShipment.TCB;

namespace PMSShipment
{
    /// <summary>
    /// SetAllWindow.xaml 的交互逻辑
    /// </summary>
    public partial class SetAllWindow : Window
    {
        public SetAllWindow()
        {
            InitializeComponent();
            var list = new List<string>();
            PMSMethods.SetListDS<PMSCommon.DeliveryItemTCBState>(list);
            CboTrackState.ItemsSource = list;
        }

        private DcDelivery currentDelivery;

        public void SetModel(DcDelivery delivery)
        {
            if (delivery != null)
            {
                currentDelivery = delivery;
                TxtDeliveryName.Text = delivery.DeliveryName;
                CboTrackState.SelectedItem = "";
            }

        }
        
        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (currentDelivery != null)
                {
                    using (var s = new TCBServiceClient())
                    {
                        var list = s.GetDeliveryItemTCBByDeliveryID(currentDelivery.ID);
                        foreach (var item in list)
                        {
                            item.TCBState = CboTrackState.SelectedItem.ToString();
                            item.TrackingHistory = CommonHelper.AppendHistory(item.TrackingHistory, item.TCBState);
                            s.UpdateDeliveryItemTCB(item);
                        }
                        Messenger.Default.Send<NotificationMessage>(null, "Refresh");
                    }
                    Close();
                }

            }
            catch (Exception)
            {

            }
        }
    }
}
