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
using LiveCharts;
using LiveCharts.Wpf;


namespace PMSClient.ToolWindow
{
    /// <summary>
    /// DensityChart.xaml 的交互逻辑
    /// </summary>
    public partial class DensityChart : Window
    {
        public DensityChart()
        {
            InitializeComponent();
        }

        private void ChkTopMost_Click(object sender, RoutedEventArgs e)
        {
            this.Topmost = (bool)ChkTopMost.IsChecked;
        }
    }
}
