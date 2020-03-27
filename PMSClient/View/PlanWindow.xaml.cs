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
using PMSClient.NewService;

namespace PMSClient.View
{
    /// <summary>
    /// PlanView.xaml 的交互逻辑
    /// </summary>
    public partial class PlanWindow : Window
    {
        public PlanWindow()
        {
            InitializeComponent();
        }

        private void DataGrid_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            try
            {
                var row = e.Row.DataContext as DcPlanExtra;
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
            catch (Exception ex)
            {
                PMSHelper.CurrentLog.Error(ex);
            }



        }



    }
}
