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
using PMSClient.Sample;

namespace PMSClient.View
{
    /// <summary>
    /// FailureView.xaml 的交互逻辑
    /// </summary>
    public partial class SampleView : UserControl
    {
        public SampleView()
        {
            InitializeComponent();
        }

        private void dg_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            DcSample sample = e.Row.DataContext as DcSample;
            switch (sample.TrackingStage)
            {
                case "未取样":
                    e.Row.Background = this.FindResource("UnPreparedBrush") as SolidColorBrush;
                    break;
                case "未核验":
                    e.Row.Background = this.FindResource("UnCheckBrush") as SolidColorBrush;
                    break;
                case "已核验":
                    e.Row.Background = this.FindResource("UnSentBrush") as SolidColorBrush;
                    break;
                default:
                    break;
            }
        }
    }
}
