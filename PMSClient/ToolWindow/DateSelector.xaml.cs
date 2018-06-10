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

namespace PMSClient.ToolWindow
{
    /// <summary>
    /// DateSelector.xaml 的交互逻辑
    /// </summary>
    public partial class DateSelector : Window
    {
        public DateSelector()
        {
            InitializeComponent();
            SelectedDate = DateTime.Today;
            DpDate.SelectedDate = SelectedDate;
        }
        private DateTime selectedDate;
        public DateTime SelectedDate
        {
            get
            {
                return selectedDate;
            }
            set
            {
                selectedDate = value;
                TxtSelectedDate.Text = $"选择的日期是{selectedDate}";
            }
        }

        private void DpDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            SelectedDate = DpDate.SelectedDate ?? DateTime.Today;
        }

        private void BtnYesterday_Click(object sender, RoutedEventArgs e)
        {
            SelectedDate = DateTime.Today.AddDays(-1);

        }

        private void BtnToday_Click(object sender, RoutedEventArgs e)
        {
            SelectedDate = DateTime.Today;
        }

        private void BtnTomorrow_Click(object sender, RoutedEventArgs e)
        {
            SelectedDate = DateTime.Today.AddDays(1);
        }

        private void BtnOK_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            Close();
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            Close();
        }
    }
}
