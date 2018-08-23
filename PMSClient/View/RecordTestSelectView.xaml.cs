using PMSClient.MainService;
using PMSClient.ViewModel.Model;
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
    /// ProductView.xaml 的交互逻辑
    /// </summary>
    public partial class RecordTestSelectView : UserControl
    {
        public RecordTestSelectView()
        {
            InitializeComponent();
        }
        private void DataGrid_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            try
            {
                var order = (RecordTestExtra)e.Row.DataContext;
                if (order != null)
                {
                    switch (order.RecordTest.State)
                    {
                        case "未录入":
                            e.Row.Background = this.FindResource("NoInputBrush") as SolidColorBrush;
                            break;
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
            catch (Exception ex)
            {
                PMSHelper.CurrentLog.Error(ex);
            }

        }
    }
}
