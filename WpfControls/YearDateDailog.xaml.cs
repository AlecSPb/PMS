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

namespace WPFControls
{
    /// <summary>
    /// YearDateDailog.xaml 的交互逻辑
    /// </summary>
    public partial class YearDateDailog : Window
    {
        public YearDateDailog()
        {
            InitializeComponent();
            LoadDefaultValues(-3);
        }

        public YearDateDailog(int monthEarly)
        {
            InitializeComponent();
            LoadDefaultValues(monthEarly);
        }
        public void LoadDefaultValues(int monthEarly)
        {
            DateTime defaultStart = DateTime.Today.AddMonths(monthEarly);
            DateTime defaultEnd = DateTime.Today;

            List<int> yearsstart = new List<int>();
            for (int i = 2017; i <= 2100; i++)
            {
                yearsstart.Add(i);
            }
            CboYearStart.ItemsSource = yearsstart;
            CboYearStart.SelectedItem = defaultStart.Year;

            List<int> monthsstart = new List<int>();
            for (int i = 1; i <= 12; i++)
            {
                monthsstart.Add(i);
            }
            CboMonthStart.ItemsSource = monthsstart;
            CboMonthStart.SelectedItem = defaultStart.Month;


            List<int> yearsend = new List<int>();
            for (int i = 2017; i <= 2100; i++)
            {
                yearsend.Add(i);
            }
            CboYearEnd.ItemsSource = yearsend;
            CboYearEnd.SelectedItem = defaultEnd.Year;

            List<int> monthsend = new List<int>();
            for (int i = 1; i <= 12; i++)
            {
                monthsend.Add(i);
            }
            CboMonthEnd.ItemsSource = monthsend;
            CboMonthEnd.SelectedItem = defaultEnd.Month;

        }

        public int YearStart { get; set; } = DateTime.Now.Year;
        public int MonthStart { get; set; } = DateTime.Now.Month;
        public int YearEnd { get; set; } = DateTime.Now.Year;
        public int MonthEnd { get; set; } = DateTime.Now.Month;
        private void BtnFill_Click(object sender, RoutedEventArgs e)
        {

            YearStart = (int)CboYearStart.SelectedItem;
            MonthStart = (int)CboMonthStart.SelectedItem;
            YearEnd = (int)CboYearEnd.SelectedItem;
            MonthEnd = (int)CboMonthEnd.SelectedItem;

            if (new DateTime(YearStart, MonthStart, 1) > new DateTime(YearEnd, MonthEnd, 1))
            {
                XSHelper.XS.MessageBox.ShowWarning("开始年月必须小于结束年月");
                return;
            }
            DialogResult = true;
            this.Close();
        }
    }
}
