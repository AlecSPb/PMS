using PMSClient.ExtraService;
using PMSClient.ViewModel.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    /// FeedbackView.xaml 的交互逻辑
    /// </summary>
    public partial class ToDoView : UserControl
    {
        public ToDoView()
        {
            InitializeComponent();
        }

        private void DataGrid_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            try
            {
                var model = (DcToDo)e.Row.DataContext;
                if (model != null)
                {
                    if (model.Priority.Contains("优先"))
                        e.Row.Background = this.FindResource("EmergencyBrush") as SolidColorBrush;
                    switch (model.Status)
                    {
                        case "暂停":
                            e.Row.Background = this.FindResource("PausedBrush") as SolidColorBrush;
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

        private void Hyperlink_Click(object sender, RoutedEventArgs e)
        {
            Hyperlink link = sender as Hyperlink;
            Process.Start(new ProcessStartInfo(link.NavigateUri.AbsoluteUri));
        }
    }
}
