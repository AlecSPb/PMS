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

namespace PMSClient.Components.ExcelOutputHelper.Dialogs
{
    /// <summary>
    /// YearDateDailog.xaml 的交互逻辑
    /// </summary>
    public partial class YearDateDailog : Window
    {
        public YearDateDailog()
        {
            InitializeComponent();
            LoadDefaultValues();
        }

        public void LoadDefaultValues()
        {
            List<int> years = new List<int>();
            for (int i = 2017; i < 2100; i++)
            {
                years.Add(i);
            }
            CboYear.ItemsSource = years;
            CboYear.SelectedItem = DateTime.Now.Year;

            List<int> months = new List<int>();
            for (int i = 1; i < 12; i++)
            {
                months.Add(i);
            }
            CboMonth.ItemsSource = months;
            CboMonth.SelectedItem = DateTime.Now.Month;


        }

        public int Year { get; set; } = DateTime.Now.Year;
        public int Month { get; set; } = DateTime.Now.Month;
        private void BtnFill_Click(object sender, RoutedEventArgs e)
        {
            Year = (int)CboYear.SelectedItem;
            Month = (int)CboMonth.SelectedItem;
            DialogResult = true;
            this.Close();
        }
    }
}
