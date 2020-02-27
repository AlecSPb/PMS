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
using PMSClient.ExtraService;
using PMSClient.MainService;

namespace PMSClient.View
{
    /// <summary>
    /// FailureView.xaml 的交互逻辑
    /// </summary>
    public partial class PMICounterView : UserControl
    {
        public PMICounterView()
        {
            InitializeComponent();
        }

        private void dg_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            try
            {
                var counter = ((DcPMICounter)e.Row.DataContext);
                int warning_count = int.Parse(TxtAlarmValue.Text.Trim());
                if (counter != null)
                {
                    if (counter.ItemCount < warning_count)
                    {
                        e.Row.Background = this.FindResource("InventoryWarningBrush") as SolidColorBrush;
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
