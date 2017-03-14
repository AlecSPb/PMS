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
using PMSDesktopClient.PMSMainService;
using PMSCommon;
using PMSDesktopClient.ViewModel;

namespace PMSClient.ViewForDesktop
{
    /// <summary>
    /// MaterialOrderView.xaml 的交互逻辑
    /// </summary>
    public partial class MaterialOrderView : UserControl
    {
        public MaterialOrderView()
        {
            InitializeComponent();
        }

        private void DataGrid_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            DcMaterialOrder order = (DcMaterialOrder)e.Row.DataContext;
            if (order != null)
            {
                switch (order.State)
                {
                    case "UnChecked":
                        e.Row.Background = this.FindResource("UnCheckedBrush") as SolidColorBrush;
                        break;
                    case "Paused":
                        e.Row.Background = this.FindResource("PausedBrush") as SolidColorBrush;
                        break;
                    case "UnCompleted":
                        e.Row.Background = this.FindResource("UnCompletedBrush") as SolidColorBrush;
                        break;
                    case "Completed":
                        e.Row.Background = this.FindResource("CompletedBrush") as SolidColorBrush;
                        break;
                    default:
                        break;
                }
                if (order.State==OrderState.UnCompleted.ToString() && order.Priority ==OrderPriority.Emergency.ToString())
                {
                    e.Row.Background = this.FindResource("EmergencyBrush") as SolidColorBrush;
                }

            }
        }




    }
}
