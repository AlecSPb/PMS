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
            this.DataContext = new ScanInputVM();
        }

        private void ChkTopMost_Click(object sender, RoutedEventArgs e)
        {
            this.Topmost = !this.Topmost;
        }

        private void DataGrid_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            try
            {
                var isValid = ((LotModel)e.Row.DataContext).IsValid;
                if (!isValid)
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
