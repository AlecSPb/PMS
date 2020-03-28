using PMSClient.NewService;
using PMSClient.ViewModel;
using PMSCommon;
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
    /// OrderSelectView.xaml 的交互逻辑
    /// </summary>
    public partial class MissonSelectView : UserControl
    {
        public MissonSelectView()
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



    }
}
