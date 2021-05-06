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
using PMSAnalysis.Models;

namespace PMSAnalysis
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }


        private double rectSize = 20;

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var h = new Services.AnalysisHelper();
            var rr = h.GetAnalysis();

            double left = 0;
            double top = 0;
            double step = 22;
            int count = 0;
            double currenttop = 0;
            foreach (var item in rr)
            {
                AddRect(left, top, item.A);
                top += step;
                AddRect(left, top, item.B);
                top += step;
                AddRect(left, top, item.C);
                top += step;
                AddRect(left, top, item.D);
                top += step;
                AddRect(left, top, item.E);
                top += step;
                AddRect(left, top, item.F);

                count++;
                if (count % 31 == 0)
                {
                    currenttop += (rectSize+2) * 6 + 10;
                    left = 0;
                }
                else
                {
                    left += step;
                    top = currenttop;
                }

            }
        }


        private void AddRect(double l,double t,PlanResult ptype)
        {
            Rectangle r = new Rectangle();
            r.Height = rectSize;
            r.Width = rectSize;
            r.Stroke = Brushes.Blue;
            r.StrokeThickness = 1.0;

            switch (ptype)
            {
                case PlanResult.Empty:
                    break;
                case PlanResult.W1:
                    r.Fill = Brushes.Gray;
                    break;
                case PlanResult.W2_Success:
                    r.Fill = Brushes.Yellow;
                    break;
                case PlanResult.W2_Fail:
                    r.Fill = Brushes.Black;
                    break;
                default:
                    break;
            }

            r.SetValue(Canvas.LeftProperty, l);
            r.SetValue(Canvas.TopProperty, t);
            mainCanvas.Children.Add(r);
        }



    }
}
