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
    /// PressureChangeTool.xaml 的交互逻辑
    /// </summary>
    public partial class PressureChangeTool : Window
    {
        public PressureChangeTool()
        {
            InitializeComponent();
        }

        private void diameter1_TextChanged(object sender, TextChangedEventArgs e)
        {
            Calculate();
        }

        private void Calculate()
        {
            double d1 = 0, d2 = 0, t1 = 0, t2 = 0, p1 = 0, p2 = 0;
            try
            {
                d1 = double.Parse(diameter1.Text.Trim());
                d2 = double.Parse(diameter2.Text.Trim());
                t1 = double.Parse(ton1.Text.Trim());


                t2 = t1 * d2*d2 / d1/d1;
                p1 = t1 * 10 * 1000 / (Math.PI * d1 * d1 / 4 / 1000000) / 1000000;
                p2 = t1 * 1000 / (Math.PI * d1 * d1 / 4 / 100);

                ton2.Text = t2.ToString("F3");
                pressure1.Text = p1.ToString("F3");
                pressure2.Text = p2.ToString("F3");
            }
            catch (Exception ex)
            {
                PMSHelper.CurrentLog.Error(ex);
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Calculate();
        }
    }
}
