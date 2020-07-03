using System;
using System.Windows.Controls;
using System.Windows.Media;
using PMSClient.NewService;
using PMSCommon;

namespace PMSClient.View
{
    /// <summary>
    /// OrderView.xaml 的交互逻辑
    /// </summary>
    public partial class OrderView : UserControl
    {
        public OrderView()
        {
            InitializeComponent();
        }

        private void DataGrid_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            try
            {
                DcOrder order = (DcOrder)e.Row.DataContext;
                if (order != null)
                {
                    switch (order.State)
                    {
                        case "未核验":
                            e.Row.Background = this.FindResource("UnCheckedBrush") as SolidColorBrush;
                            break;
                        case "暂停":
                            e.Row.Background = this.FindResource("PausedBrush") as SolidColorBrush;
                            break;
                        case "取消":
                            e.Row.Background = this.FindResource("CancelledBrush") as SolidColorBrush;
                            break;
                        case "未完成":
                            e.Row.Background = this.FindResource("UnCompletedBrush") as SolidColorBrush;
                            break;
                        case "生产完成":
                            e.Row.Background = this.FindResource("VHPCompletedBrush") as SolidColorBrush;
                            break;
                        case "最终完成":
                            e.Row.Background = this.FindResource("CompletedBrush") as SolidColorBrush;
                            break;
                        default:
                            break;
                    }
                    if (order.State == OrderState.未完成.ToString() && order.Priority.Contains("紧急"))
                    {
                        e.Row.Background = this.FindResource("EmergencyBrush") as SolidColorBrush;
                    }

                }

            }
            catch (Exception ex)
            {
                PMSHelper.CurrentLog.Error(ex);
            }

        }

        private void BtnLaserNeed_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            try
            {
                Button btn = sender as Button;
                if (btn != null)
                {
                    var dialog = new Components.LaserNeed.LaserNeedResultReadOnly();
                    dialog.KeyStrings = btn.Content.ToString();
                    dialog.ShowDialog();
                }
            }
            catch (Exception)
            {

            }

        }
    }
}
