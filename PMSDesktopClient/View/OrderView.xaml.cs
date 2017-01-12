using PMSDesktopClient.ViewModel;
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
using PMSDesktopClient.ServiceReference;
using PMSCommon;

namespace PMSDesktopClient.View.Sales
{
    /// <summary>
    /// OrderView.xaml 的交互逻辑
    /// </summary>
    public partial class OrderView : UserControl
    {
        public OrderView()
        {
            InitializeComponent();
            this.DataContext = new OrderVM();
        }

        private void DataGrid_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            DcOrder order = (DcOrder)e.Row.DataContext;
            if (order != null)
            {
                var orderBrush = new CommonBrushes();
                switch (order.State)
                {
                    case (int)ModelState.Stop:
                        e.Row.Background = orderBrush.StopBrush;
                        break;
                    case (int)ModelState.UnCompleted:
                        e.Row.Background = orderBrush.UnCompletedBrush;
                        break;
                    case (int)ModelState.Completed:
                        e.Row.Background = orderBrush.CompletedBrush;
                        break;
                    default:
                        break;
                }

            }

        }
    }
}
