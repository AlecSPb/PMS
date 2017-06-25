using PMSCommon;
using PMSClient.MainService;
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
using PMSClient.ViewModel;

namespace PMSClient.View
{
    /// <summary>
    /// MissonView.xaml 的交互逻辑
    /// </summary>
    public partial class MissonUnCompletedView : UserControl
    {
        public MissonUnCompletedView()
        {
            InitializeComponent();
        }
        private void DataGrid_LoadingRow(object sender, DataGridRowEventArgs e)
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
                    case "未完成":
                        e.Row.Background = this.FindResource("UnCompletedBrush") as SolidColorBrush;
                        break;
                    case "生产完成":
                        e.Row.Background = this.FindResource("CompletedBrush") as SolidColorBrush;
                        break;
                    case "完成":
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

        private void dgplans_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            var plan = (DcPlanVHP)e.Row.DataContext;
            if (plan != null)
            {
                switch (plan.State)
                {
                    case "未核验":
                        e.Row.Background = this.FindResource("UnCheckedBrush") as SolidColorBrush;
                        break;
                    case "已核验":
                        e.Row.Background = this.FindResource("CheckedBrush") as SolidColorBrush;
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
