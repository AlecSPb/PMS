using PMSClient.DataProcess;
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

namespace PMSClient.DataProcess.ScanInput
{
    /// <summary>
    /// ScanInput.xaml 的交互逻辑
    /// </summary>
    public partial class ScanInput : Window
    {
        public ScanInput()
        {
            InitializeComponent();
        }

        private void ChkTopMost_Click(object sender, RoutedEventArgs e)
        {
            this.Topmost = !this.Topmost;
        }

        private void DataGrid_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            try
            {
                var msg = ((LotModel)e.Row.DataContext).ExceptionMessage;
                if (msg != "")
                {
                    e.Row.Background = this.FindResource("UnCheckedBrush") as SolidColorBrush;
                }
            }
            catch (Exception ex)
            {
                PMSHelper.CurrentLog.Error(ex);
            }
        }
    }
}
