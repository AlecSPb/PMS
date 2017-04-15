using PMSClient.MainService;
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

namespace PMSClient.ViewForDesktop
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
                    case "完成":
                        e.Row.Background = this.FindResource("CompletedBrush") as SolidColorBrush;
                        break;
                    default:
                        break;
                }
                if (order.State == OrderState.未完成.ToString() && order.Priority == OrderPriority.紧急.ToString())
                {
                    e.Row.Background = this.FindResource("EmergencyBrush") as SolidColorBrush;
                }

            }

        }



    }
}
