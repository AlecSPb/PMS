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
using PMSClient.OutsideProcessService;

namespace PMSClient.View
{
    /// <summary>
    /// OutSourceView.xaml 的交互逻辑
    /// </summary>
    public partial class OutsideProcessView : UserControl
    {
        public OutsideProcessView()
        {
            InitializeComponent();
        }

        private void DataGrid_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            try
            {
                var row = e.Row.DataContext as DcOutsideProcess;
                if (row != null)
                {
                    if (row.State == PMSCommon.OutsideProcessState.未完成.ToString())
                    {
                        e.Row.Background = this.FindResource("UnCompletedBrush") as SolidColorBrush;
                    }
                    else if (row.State == PMSCommon.OutsideProcessState.暂停.ToString())
                    {
                        e.Row.Background = this.FindResource("PausedBrush") as SolidColorBrush;
                    }
                    else if (row.State == PMSCommon.OutsideProcessState.未录完.ToString())
                    {
                        e.Row.Background = this.FindResource("NoInputBrush") as SolidColorBrush;
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
