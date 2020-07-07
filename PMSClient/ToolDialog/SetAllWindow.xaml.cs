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
using PMSClient.MainService;

namespace PMSClient
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
                    using (var s = new DeliveryServiceClient())
                    {
                        var list = s.GetDeliveryItemByDeliveryID(currentDelivery.ID);
                        foreach (var item in list)
                        {
                            item.TCBState = CboTrackState.SelectedItem.ToString();
                            item.TrackingHistory = PMSClient.ToolDialog.CommonHelper.AppendHistory(item.TrackingHistory, item.TCBState);
                            s.UpdateDeliveryItem(item);
                        }
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
