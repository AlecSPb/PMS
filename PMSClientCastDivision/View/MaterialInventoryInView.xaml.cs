using PMSClient.SanjieService;
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
    /// MaterialInventory.xaml 的交互逻辑
    /// </summary>
    public partial class MaterialInventoryInView : UserControl
    {
        public MaterialInventoryInView()
        {
            InitializeComponent();
        }

        private void DataGrid_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            var order = (DcMaterialInventoryIn)e.Row.DataContext;
            if (order != null)
            {
                switch (order.State)
                {
                    case "暂入库":
                        e.Row.Background = new SolidColorBrush(Colors.Orange);
                        break;
                    default:
                        break;
                }

            }
        }
    }
}
