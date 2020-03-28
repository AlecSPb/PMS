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

namespace PMSClient.View
{
    /// <summary>
    /// RecordBondingView.xaml 的交互逻辑
    /// </summary>
    public partial class RecordBondingView : UserControl
    {
        public RecordBondingView()
        {
            InitializeComponent();
        }
        private void DataGrid_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            try
            {
                var order = (DcRecordBonding)e.Row.DataContext;
                if (order != null)
                {
                    switch (order.State)
                    {
                        case "未完成":
                            e.Row.Background = this.FindResource("UnCompletedBrush") as SolidColorBrush;
                            break;
                        case "未录完":
                            e.Row.Background = this.FindResource("NoInputBrush") as SolidColorBrush;
                            break;
                        case "暂停":
                            e.Row.Background = this.FindResource("PausedBrush") as SolidColorBrush;
                            break;
                        case "失败":
                            e.Row.Background = this.FindResource("FailedBrush") as SolidColorBrush;
                            break;
                        case "最终完成":
                            e.Row.Background = this.FindResource("CompletedBrush") as SolidColorBrush;
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
