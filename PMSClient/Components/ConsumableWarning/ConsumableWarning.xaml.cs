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
using PMSClient.ConsumableService;

namespace PMSClient.Components.ConsumableWarning
{
    /// <summary>
    /// ConsumableWarning.xaml 的交互逻辑
    /// </summary>
    public partial class ConsumableWarning : Window
    {
        public ConsumableWarning()
        {
            InitializeComponent();
        }
        private void DataGrid_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            try
            {
                DcConsumableInventory model = (DcConsumableInventory)e.Row.DataContext;
                if (model != null)
                {
                    if (model.MaxWarningQuantity > model.MinWarningQuantity)
                    {
                        if (model.Quantity > model.MaxWarningQuantity)
                        {
                            e.Row.Background = this.FindResource("InventoryMaxWarningBrush") as SolidColorBrush;
                        }
                        if (model.Quantity < model.MinWarningQuantity)
                        {
                            e.Row.Background = this.FindResource("InventoryMinWarningBrush") as SolidColorBrush;
                        }
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
