using PMSClient.ExtraService;
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
    /// FailureView.xaml 的交互逻辑
    /// </summary>
    public partial class RemainInventoryView : UserControl
    {
        public RemainInventoryView()
        {
            InitializeComponent();
        }

        private void dg_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            try
            {
                var order = (DcRemainInventory)e.Row.DataContext;
                if (order != null)
                {

                    switch (order.State)
                    {
                        case "库存":
                            e.Row.Background = this.FindResource("UnCompletedBrush") as SolidColorBrush;
                            break;
                        case "出库":
                            e.Row.Background = this.FindResource("CheckedBrush") as SolidColorBrush;
                            break;
                        default:
                            break;
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
