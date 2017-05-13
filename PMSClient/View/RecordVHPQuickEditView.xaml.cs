using GalaSoft.MvvmLight.Messaging;
using PMSClient.MainService;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
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
    /// RecordVHPQuickEdit.xaml 的交互逻辑
    /// </summary>
    public partial class RecordVHPQuickEditView : UserControl
    {
        public RecordVHPQuickEditView()
        {
            InitializeComponent();
            _timer = new Timer();
        }
        private Timer _timer;
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            _timer.Interval = 1000;
            _timer.Elapsed += _timer_Elapsed;
            _timer.Start();
        }

        private void _timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            this.Dispatcher.Invoke(() =>
            {
                txtCurrentTime.Text = $"现在时间:{DateTime.Now.ToString("HH:mm:ss")}";
            });
        }

        private void DataGrid_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            var row = e.Row.DataContext as DcPlanWithMisson;
            if (row != null)
            {
                var today = DateTime.Now.Date;
                var rowDate = row.Plan.PlanDate.Date;
                if (rowDate == today.Date)
                {
                    e.Row.Background = this.FindResource("TodayBrush") as SolidColorBrush;
                }
                else if (rowDate == today.Date.AddDays(-1))
                {
                    e.Row.Background = this.FindResource("YesterdayBrush") as SolidColorBrush;
                }
                else if (rowDate == today.Date.AddDays(1))
                {
                    e.Row.Background = this.FindResource("TomorrowBrush") as SolidColorBrush;
                }
            }


        }
    }
}
