using System;
using System.Collections.Generic;
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
    /// NavigationView.xaml 的交互逻辑
    /// </summary>
    public partial class NavigationView : UserControl
    {
        public NavigationView()
        {
            InitializeComponent();
            InitializeTimer();
        }

        private void InitializeTimer()
        {
            ShowNow();

            Timer timer = new Timer();
            timer.Interval = 60 * 1000;
            timer.Elapsed += Timer_Elapsed;
            timer.Start();
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            App.Current.Dispatcher.Invoke(() =>
            {
                ShowNow();
            });
        }
        private void ShowNow()
        {
            CurrentTime.Text = $"时间:{DateTime.Now.ToShortTimeString()}";
        }

        private void BtnTargetCutterSimlulator_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start("TargetCutterSimulator.exe");
            }
            catch (Exception)
            {
                   
            }
        }
    }
}
