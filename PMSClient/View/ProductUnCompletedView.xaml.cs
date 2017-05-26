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
using PMSClient.MainService;
using PMSCommon;
using PMSClient.ViewModel;

namespace PMSClient.View
{
    /// <summary>
    /// MaterialOrderView.xaml 的交互逻辑
    /// </summary>
    public partial class ProductUnCompletedView : UserControl
    {
        public ProductUnCompletedView()
        {
            InitializeComponent();
        }

        private void DataGrid_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            var order = (DcProduct)e.Row.DataContext;
            if (order != null)
            {
                switch (order.State)
                {
                    case "库存":
                        e.Row.Background = this.FindResource("StoredBrush") as SolidColorBrush;
                        break;
                    case "发货":
                        e.Row.Background = this.FindResource("DeliveredBrush") as SolidColorBrush;
                        break;
                    default:
                        break;
                }

            }
        }


    }
}
